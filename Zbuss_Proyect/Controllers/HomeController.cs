using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
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
            List<Something> somethings = new List<Something>();
            somethings.Add(new Something()
            {
                Id = 1,
                Title = "Some 1",
                Img = "b1.jpg"

            });
            somethings.Add(new Something()
            {
                Id = 2,
                Title = "Some 2",
                Img = "b2.jpg"

            });
            somethings.Add(new Something()
            {
                Id = 3,
                Title = "Some 3",
                Img = "b3.jpg"

            });
            return View(somethings);
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
