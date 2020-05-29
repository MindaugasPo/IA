using System;
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
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAssetService _assetService;
        private readonly IPortfolioService _portfolioService;
        private readonly UserManager<User> _userManager;
        public TransactionController(
            ITransactionService transactionService,
            IAssetService assetService,
            IPortfolioService portfolioService,
            UserManager<User> userManager)
        {
            _transactionService = transactionService;
            _assetService = assetService;
            _portfolioService = portfolioService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var transaction = _transactionService.Get(id);
            if (transaction == null)
            {
                return new JsonResult(new AjaxResult(){ Success = false, Message = "Transaction was not found" });
            }
            var user = await _userManager.GetUserAsync(User);
            if (transaction.Portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Transaction belongs to another user" });
            }
            var vm = new TransactionFormVM()
            {
                PortfolioId = transaction.PortfolioId,
                Assets = _assetService.GetAll().ToList(),
                Transaction = transaction,
                Portfolios = _portfolioService.GetAll(user.Id).ToList()
            };
            return PartialView("~/Views/Transaction/TransactionForm.cshtml", vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new AllTransactionsVM()
            {
                Transactions = _transactionService.GetAll(user.Id).ToList(),
                DisplayCloseData = true
            };
            return PartialView("~/Views/Transaction/AllTransactions.cshtml", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid PortfolioId)
        {
            var user = await _userManager.GetUserAsync(User);
            var portfolio = _portfolioService.Get(PortfolioId);

            if (portfolio == null || portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio belongs to another user" });
            }

            var vm = new TransactionFormVM()
            {
                PortfolioId = PortfolioId,
                Assets = _assetService.GetAll().ToList(),
                Transaction = null,
                Portfolios = _portfolioService.GetAll(user.Id).ToList()
            };
            return PartialView("~/Views/Transaction/TransactionForm.cshtml", vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public async Task<IActionResult> Create(TransactionDto transaction)
        {
            var portfolio = _portfolioService.Get(transaction.PortfolioId);
            var user = await _userManager.GetUserAsync(User);
            if (portfolio == null || portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio belongs to another user" });
            }

            _transactionService.Create(transaction);
            return new JsonResult(new AjaxResult(){ Success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Update(TransactionDto transaction)
        {
            var portfolio = _portfolioService.Get(transaction.PortfolioId);
            var user = await _userManager.GetUserAsync(User);
            if (portfolio == null || portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio belongs to another user" });
            }

            _transactionService.Update(transaction);
            return new JsonResult(new AjaxResult() { Success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Close(Guid id, decimal closePrice, DateTime closeDate)
        {
            var transaction = _transactionService.Get(id);
            var user = await _userManager.GetUserAsync(User);
            if (transaction == null || transaction.Portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio belongs to another user" });
            }
            _transactionService.Close(id, closePrice, closeDate);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Closed"});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var transaction = _transactionService.Get(id);
            var user = await _userManager.GetUserAsync(User);
            if (transaction == null || transaction.Portfolio.UserId != user.Id)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Portfolio belongs to another user" });
            }

            _transactionService.Delete(id);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Deleted" });
        }
    }
}
