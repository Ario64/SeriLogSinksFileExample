using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SeriLogSinksFileExample.Models;

namespace SeriLogSinksFileExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var max = 30;
            var numbers = new { num1 = 10, num2 = 30 };
            var sum = numbers.num1 + numbers.num2;
            _logger.Log(LogLevel.Warning, "numbers are @{numbers} and sum is: {sum}", numbers, sum);
            if (sum > max) _logger.Log(logLevel: LogLevel.Error, "sum is bigger than {max}", max);
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
