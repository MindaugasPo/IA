using System;
using System.Collections.Generic;
using IA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using Types;
using Types.DTO;

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
            var historicPortfolio = _portfolioService.GetHistoricPortfolio(id);

            if (portfolio == null)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio was not found" });
            }

            var vm = new PortfolioVM()
            {
                PortfolioId = id,
                Title = portfolio.Title,
                Transactions = portfolio.Transactions,
                HistoricTransactions = historicPortfolio?.Transactions ?? new List<TransactionDto>()
            };
            return PartialView("~/Views/Portfolio/Portfolio.cshtml", vm);
        }
    }
}
