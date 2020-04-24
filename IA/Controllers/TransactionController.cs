using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace IA.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(
            ITransactionService transactionService)
        {
            _transactionService = transactionService;
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
    }
}
