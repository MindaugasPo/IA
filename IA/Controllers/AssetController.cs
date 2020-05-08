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
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly ITransactionService _transactionService;
        private readonly IAValidatorFactory _validatorFactory;
        public AssetController(
            IAssetService assetService,
            ITransactionService transactionService,
            IAValidatorFactory validatorFactory)
        {
            _assetService = assetService;
            _transactionService = transactionService;
            _validatorFactory = validatorFactory;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("~/Views/Asset/NewAsset.cshtml");
        }

        [HttpPost]
        public IActionResult Create(AssetDto assetDto)
        {
            var validator = _validatorFactory.GetValidator(assetDto);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult(){Success = false, Message = validator.Errors()});
            }
            _assetService.Create(assetDto);
            return new JsonResult("Created");
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

            if (asset == null)
            {
                return new JsonResult(new AjaxResult() {Success = false, Message = "Asset was not found"});
            }

            var vm = new AssetPricesVM()
            {
                Asset = asset,
                AssetPrices = _assetService.GetAssetPrices(id).ToList()
            };
            return PartialView("~/Views/Asset/AssetPrices.cshtml", vm);
        }
    }
}
