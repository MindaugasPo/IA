using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using Types.DTO;

namespace IA.Controllers
{
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
        public IActionResult GetAll()
        {
            var vm = new AllTransactionsVM()
            {
                Transactions = _transactionService.GetAll().ToList()
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
            _transactionService.Create(transaction);
            return new JsonResult("Created");
        }
    }
}
