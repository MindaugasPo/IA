using System;
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
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAssetService _assetService;
        public TransactionController(
            ITransactionService transactionService,
            IAssetService assetService)
        {
            _transactionService = transactionService;
            _assetService = assetService;
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var transaction = _transactionService.Get(id);
            if (transaction == null)
            {
                return new JsonResult(new AjaxResult(){ Success = false, Message = "Transaction was not found" });
            }
            var vm = new TransactionFormVM()
            {
                PortfolioId = transaction.PortfolioId,
                Assets = _assetService.GetAll().ToList(),
                Transaction = transaction
            };
            return PartialView("~/Views/Transaction/TransactionForm.cshtml", vm);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vm = new AllTransactionsVM()
            {
                Transactions = _transactionService.GetAll().ToList(),
                DisplayCloseData = true
            };
            return PartialView("~/Views/Transaction/AllTransactions.cshtml", vm);
        }

        [HttpGet]
        public IActionResult Create(Guid PortfolioId)
        {
            var vm = new TransactionFormVM()
            {
                PortfolioId = PortfolioId,
                Assets = _assetService.GetAll().ToList(),
                Transaction = null
            };
            return PartialView("~/Views/Transaction/TransactionForm.cshtml", vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Create(TransactionDto transaction)
        {
            _transactionService.Create(transaction);
            return new JsonResult(new AjaxResult(){ Success = true });
        }

        [HttpPost]
        public IActionResult Update(TransactionDto transaction)
        {
            _transactionService.Update(transaction);
            return new JsonResult(new AjaxResult() { Success = true });
        }

        [HttpPost]
        public IActionResult Close(Guid id, decimal closePrice)
        {
            _transactionService.Close(id, closePrice);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Closed"});
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _transactionService.Delete(id);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Deleted" });
        }
    }
}
