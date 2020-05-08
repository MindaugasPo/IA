using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var vm = new NewTransactionVM()
            {
                PortfolioId = PortfolioId,
                Assets = _assetService.GetAll().ToList()
            };
            return PartialView("~/Views/Transaction/NewTransaction.cshtml", vm);
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
            return new JsonResult("Created");
        }

        [HttpPost]
        public IActionResult Close(Guid id, decimal closePrice)
        {
            _transactionService.Close(id, closePrice);
            return new JsonResult("Closed");
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _transactionService.Delete(id);
            return new JsonResult("Deleted");
        }
    }
}
