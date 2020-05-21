using System;
using System.Collections.Generic;
using System.Linq;
using IA.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetAll(Guid? selectedPortfolioId)
        {
            var allPortfolios = _portfolioService.GetAll().OrderByDescending(x => x.CreatedDateUtc).ToList();

            if (!allPortfolios.Any())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "No portfolios were found" });
            }

            var vm = new AllPortfoliosVM()
            {
                AllPortfolios = allPortfolios.Select(x => new PortfolioVM() {PortfolioId = x.Id, Title = x.Title} ).ToList()
            };

            var selectedId = selectedPortfolioId.HasValue && selectedPortfolioId.Value != Guid.Empty
                ? selectedPortfolioId.Value
                : allPortfolios.First().Id;
            var selectedPortfolio = _portfolioService.Get(selectedId);
            var historicPortfolio = _portfolioService.GetHistoricPortfolio(selectedPortfolio.Id);

            vm.SelectedPortfolio = new PortfolioVM()
            {
                PortfolioId = selectedPortfolio.Id,
                Title = selectedPortfolio.Title,
                Transactions = selectedPortfolio.Transactions,
                HistoricTransactions = historicPortfolio?.Transactions ?? new List<TransactionDto>()
            };

            return PartialView("~/Views/Portfolio/Portfolios.cshtml", vm);
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var portfolio = _portfolioService.Get(id);
            var historicPortfolio = _portfolioService.GetHistoricPortfolio(id);
            var vm = new PortfolioVM()
            {
                PortfolioId = portfolio.Id,
                Title = portfolio.Title,
                Transactions = portfolio.Transactions,
                HistoricTransactions = historicPortfolio?.Transactions ?? new List<TransactionDto>()
            };
            return PartialView("~/Views/Portfolio/PortfolioData.cshtml", vm);
        }
    }
}
