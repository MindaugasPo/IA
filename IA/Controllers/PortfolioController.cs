using System;
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

            if (portfolio == null)
            {
                return new JsonResult("Portfolio was not found");
            }

            var vm = new PortfolioVM()
            {
                PortfolioId = id,
                Title = portfolio.Title,
                Transactions = portfolio.Transactions
            };
            return PartialView("~/Views/Portfolio/Portfolio.cshtml", vm);
        }
    }
}
