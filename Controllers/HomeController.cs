using CarBill.bussinesData;
using CarBill.bussinesData.Models;
using CarBill.Models;
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

        public HomeController(ILogger<HomeController> logger, bussinesContext db)
        {
            _logger = logger;
            _db = db;
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
            // Process the form data...

            // Instead of redirecting server-side, return a JSON response with the target URL
            return Json(new { redirectUrl = Url.Action("Index", "Home"), message = "Working perfect!" });
        }

        public IActionResult yourBills()
        {

            return View();
        }



        public IActionResult Privacy()
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