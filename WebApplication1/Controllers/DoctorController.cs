using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DoctorController : Controller
    {
        [HttpPost("/FeverCheck")]
        public IActionResult FeverCheck(float temperature, string scale = "Celsius")
        {
            string message = TemperatureChecker.CheckTemperature(temperature, scale);
            ViewData["Message"] = message;
            return View();
        }

        [HttpGet("/FeverCheck")]
        public IActionResult FeverCheck()
        {
            return View();
        }
    }
}










