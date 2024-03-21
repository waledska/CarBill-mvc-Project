using CarBill.bussinesData;
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

            // get the spare Parts
            var spareParts = _db.SpareParts.Include(s => s.Type).ToList();
            billFormData.sparePartsWithCat = spareParts;

            return View(billFormData);
        }

        // API endpoint
        public ActionResult GetSparePartsByCategory(int categoryId)
        {
            var spareParts = _db.SpareParts.Where(sp => sp.TypeId == categoryId).ToList();
            return Ok(spareParts);
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