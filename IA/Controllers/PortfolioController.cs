using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.Filters;
using IA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Types;
using Types.DTO;
using Types.Entities;

namespace IA.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly UserManager<User> _userManager;
        public PortfolioController(
            IPortfolioService portfolioService,
            UserManager<User> userManager)
        {
            _portfolioService = portfolioService;
            _userManager = userManager;
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
        public async Task<IActionResult> Edit(Guid id)
        {
            var portfolio = _portfolioService.Get(id);
            var user = await _userManager.GetUserAsync(User);
            if (portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio belongs to another user" });
            }
            var vm = new PortfolioFormVM()
            {
                PortfolioId = portfolio.Id,
                Title = portfolio.Title,
                NewPortfolio = false,
                UserId = user.Id
            };
            return PartialView("~/Views/Portfolio/PortfolioForm.cshtml", vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public async Task<IActionResult> Create(PortfolioDto portfolio)
        {
            var user = await _userManager.GetUserAsync(User);
            if (portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Unable to create portfolio for another user" });
            }
            portfolio.Transactions = new List<TransactionDto>();
            _portfolioService.Create(portfolio);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Created" });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new PortfolioFormVM()
            {
               PortfolioId = Guid.NewGuid(), 
               Title = "", 
               NewPortfolio = true,
               UserId = user.Id
            };
            return PartialView("~/Views/Portfolio/PortfolioForm.cshtml", vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid? selectedPortfolioId)
        {
            var user = await _userManager.GetUserAsync(User);
            var allPortfolios = _portfolioService.GetAll(user.Id).OrderByDescending(x => x.CreatedDateUtc).ToList();
            
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
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            var portfolio = _portfolioService.GetCurrent(id);
            if (portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Unable to create portfolio for another user" });
            }
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
