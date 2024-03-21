using CarBill.bussinesData;
using CarBill.Models;
using Microsoft.AspNetCore.Mvc;
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
            // get the carData
            var carData = _db.Cars.FirstOrDefault(i => i.Id == id);

            return View(carData);
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