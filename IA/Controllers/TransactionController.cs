using System;
using System.Linq;
using IA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using Types;
using Types.DTO;
using ValidationService;

namespace IA.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAssetService _assetService;
        private readonly IAValidatorFactory _validatorFactory;
        public TransactionController(
            ITransactionService transactionService,
            IAssetService assetService,
            IAValidatorFactory validatorFactory)
        {
            _transactionService = transactionService;
            _assetService = assetService;
            _validatorFactory = validatorFactory;
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
        public IActionResult Create(TransactionDto transaction)
        {
            var validator = _validatorFactory.GetValidator(transaction);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
            }

            _transactionService.Create(transaction);
            return new JsonResult(new AjaxResult(){ Success = true });
        }

        [HttpPost]
        public IActionResult Update(TransactionDto transaction)
        {
            var validator = _validatorFactory.GetValidator(transaction);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
            }

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
