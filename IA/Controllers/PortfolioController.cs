using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace IA.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var portfolio = _portfolioService.GetCurrent(id);
            var vm = new PortfolioVM()
            {
                Title = portfolio.Title,
                Transactions = portfolio.Transactions
            };
            return PartialView("~/Views/Portfolio/Portfolio.cshtml", vm);
        }
    }
}
