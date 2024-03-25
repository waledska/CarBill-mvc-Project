using CarBill.bussinesData;
using CarBill.bussinesData.Models;
using CarBill.Models;
using CarBill.Services;
using CarBill.vm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarBill.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly bussinesContext _db;
        private readonly ITransferPhotosToPathWithStoreService _imgServ;

        public HomeController(ILogger<HomeController> logger, bussinesContext db, ITransferPhotosToPathWithStoreService imgServ)
        {
            _logger = logger;
            _db = db;
            _imgServ = imgServ;
        }

        public IActionResult Index()
        {
            return View();
        }

        // get end points format => APIName/id
        public ActionResult ShowCarData(int id)
        {
            BillFormData billFormData = new BillFormData();

            // get the carData in BillFormData
            var cardata = _db.Cars.FirstOrDefault(i => i.Id == id);
            billFormData.carData = cardata;

            // get the categories
            var Cats = _db.MaintananceTypes.ToList();
            billFormData.cats = Cats;


            return View(billFormData);
        }

        // APIs endpoint
        public ActionResult GetSparePartsByCategory(int categoryId)
        {
            var spareParts = _db.SpareParts.Where(sp => sp.TypeId == categoryId).ToList();
            return Ok(spareParts);
        }

        [HttpPost]
        public ActionResult SaveNewBill(BillFormData form)
        {
            var errorMessage = "";
            // first save new bill in db
            
            // validation on discount_percentage(0-100)
            if(form.additionBillData.discountRate < 0 || form.additionBillData.discountRate > 100)
                return Json("discount percentage must be between 0 and 100");

            // validate car_id
            if(! (_db.Cars.Any(c => c.Id == form.carData.Id) ))
                return Json("some thing went wrong, there is no cars with this car id");

            // save the video in folder videos 
            var videoURL = _imgServ.SaveVideoFile(form.additionBillData.videoInFormFile);

            // validation for the video
            if( videoURL.StartsWith("error") )
                return Json(videoURL);

            // add the bill to the db to get the bill ID
            var theNewBill = new Bill
            {
                CarId = form.carData.Id,
                Comment = form.additionBillData.comment,
                DicountPrecentage = form.additionBillData.discountRate,
                LastReading = form.carData.LastReading,
                MaintancePeriod = form.additionBillData.maintancePeriod,
                TotalPrice = form.additionBillData.totalAmount,
                VideoUrl = videoURL,
            };

            _db.Bills.Add(theNewBill);
            _db.SaveChanges();

            //var thisBillRows = new List<BillRow>();

            // create the list of bill row
            foreach (var billRow in form.additionBillData.billRows)
            {
                // validation on Spare part ID
                if(! (_db.SpareParts.Any(s => s.Id == billRow.sparePartId) ))
                    return Json("some thing went wrong, there is no spare parts with this spare part id");

                // add bill row to the List then save db
                var theBillRow = (new BillRow
                {
                    Amount = billRow.amount,
                    sparePartId = billRow.sparePartId,
                    TotalPriceForBillRow = billRow.total_priceForBillRow,
                    BillId = theNewBill.Id,
                });


                _db.BillRows.Add(theBillRow);
                _db.SaveChanges();

                // store the images for this bill row in folder images
                List<BillRowImage> imagesForTheBillRow= new List<BillRowImage>();
                var billRowImages = _imgServ.GetPhotosPath(billRow.PhotosInFormFiles);
                // check if images stored successfully
                foreach (var imgUrl in billRowImages)
                {
                    if (imgUrl.StartsWith("error"))
                        return Json(imgUrl);
                    imagesForTheBillRow.Add(new BillRowImage
                    { 
                        ImgUrl = imgUrl,
                        BillRowId = theBillRow.Id
                    });
                }

                // store this list of billRowImages
                _db.BillRowImages.AddRange(imagesForTheBillRow);
                _db.SaveChanges();
                
            }

            // update the car reading to the current reading
            var currentCar = _db.Cars.FirstOrDefault(c => c.Id == form.carData.Id);
            currentCar.LastReading = form.additionBillData.currentReading;
            _db.Cars.Update(currentCar);
            _db.SaveChanges();

            // Instead of redirecting server-side, return a JSON response with the target URL
            return Json(new { redirectUrl = Url.Action("Index", "Home"), message = "Working perfect!" });
        }

        public IActionResult yourBills()
        {

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}