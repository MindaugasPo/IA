using System;
using System.Collections.Generic;
using System.Linq;
using IA.Filters;
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

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _portfolioService.Delete(id);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Deleted" });
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Update(PortfolioDto portfolio)
        {
            _portfolioService.Update(portfolio);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Created" });
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var portfolio = _portfolioService.Get(id);
            var vm = new PortfolioFormVM()
            {
                PortfolioId = portfolio.Id,
                Title = portfolio.Title,
                NewPortfolio = false
            };
            return PartialView("~/Views/Portfolio/PortfolioForm.cshtml", vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Create(PortfolioDto portfolio)
        {
            portfolio.Transactions = new List<TransactionDto>();
            _portfolioService.Create(portfolio);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Created" });
        }

        [HttpGet]
        public IActionResult Create()
        {
           var vm = new PortfolioFormVM()
           {
               PortfolioId = Guid.NewGuid(), 
               Title = "", 
               NewPortfolio = true
           };
            return PartialView("~/Views/Portfolio/PortfolioForm.cshtml", vm);
        }

        [HttpGet]
        public IActionResult GetAll(Guid? selectedPortfolioId)
        {
            var allPortfolios = _portfolioService.GetAll().OrderByDescending(x => x.CreatedDateUtc).ToList();
            
            var vm = new AllPortfoliosVM()
            {
                AllPortfolios = allPortfolios.Select(x => new PortfolioVM() {PortfolioId = x.Id, Title = x.Title} ).ToList()
            };

            if (allPortfolios.Any())
            {
                var selectedId = selectedPortfolioId.HasValue && selectedPortfolioId.Value != Guid.Empty
                    ? selectedPortfolioId.Value
                    : allPortfolios.First().Id;
                var selectedPortfolio = _portfolioService.GetCurrent(selectedId) ?? _portfolioService.GetCurrent(allPortfolios.First().Id);
                var historicPortfolio = _portfolioService.GetHistoricPortfolio(selectedPortfolio.Id);

                vm.SelectedPortfolio = new PortfolioVM()
                {
                    PortfolioId = selectedPortfolio.Id,
                    Title = selectedPortfolio.Title,
                    Transactions = selectedPortfolio.Transactions,
                    HistoricTransactions = historicPortfolio?.Transactions ?? new List<TransactionDto>()
                };
            }
            
            return PartialView("~/Views/Portfolio/Portfolios.cshtml", vm);
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var portfolio = _portfolioService.GetCurrent(id);
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
