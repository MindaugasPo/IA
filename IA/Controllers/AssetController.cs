using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace IA.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly ITransactionService _transactionService;
        public AssetController(
            IAssetService assetService,
            ITransactionService transactionService)
        {
            _assetService = assetService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vm = new AllAssetsVM()
            {
                Assets = _assetService.GetAll().ToList()
            };
            return PartialView("~/Views/Asset/AllAssets.cshtml", vm);
        }

        [HttpGet]
        public IActionResult GetAssetPrices(Guid id)
        {
            var asset = _assetService.Get(id);
            var assetPrices = _assetService.GetAssetPrices(id);
            var vm = new AssetPricesVM()
            {
                Asset = _assetService.Get(id),
                AssetPrices = _assetService.GetAssetPrices(id).ToList()
            };
            return PartialView("~/Views/Asset/AssetPrices.cshtml", vm);
        }
    }
}
