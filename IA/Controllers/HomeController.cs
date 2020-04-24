using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IA.Models;
using IA.ViewModels;
using IADbContext;
using Services;
using Types;
using Types.Entities;

namespace IA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetService _assetService;
        
        public HomeController(
            ILogger<HomeController> logger, 
            IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
