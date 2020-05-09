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
    public class AssetPriceController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IAssetPriceService _assetPriceService;
        private readonly IAValidatorFactory _validatorFactory;

        public AssetPriceController(
            IAssetService assetService,
            IAValidatorFactory validatorFactory,
            IAssetPriceService assetPriceService)
        {
            _assetService = assetService;
            _validatorFactory = validatorFactory;
            _assetPriceService = assetPriceService;
        }

        [HttpPost]
        public IActionResult Delete(Guid assetPriceId)
        {
            _assetPriceService.Delete(assetPriceId);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Deleted" });
        }
        [HttpGet]
        public IActionResult Create(Guid assetId)
        {
            var vm = new AssetPriceFormVM() { AssetPriceDto = null, AssetId = assetId };
            return PartialView("~/Views/Asset/AssetPriceForm.cshtml", vm);
        }
        [HttpPost]
        public IActionResult Create(AssetPriceDto assetPriceDto)
        {
            var validator = _validatorFactory.GetValidator(assetPriceDto);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
            }
            _assetPriceService.Create(assetPriceDto);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Updated" });
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var assetPrice = _assetPriceService.Get(id);
            if (assetPrice == null)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Asset price was not found" });
            }
            var vm = new AssetPriceFormVM() { AssetPriceDto = assetPrice, AssetId = assetPrice.Asset.Id };
            return PartialView("~/Views/Asset/AssetPriceForm.cshtml", vm);
        }
        [HttpPost]
        public IActionResult Update(AssetPriceDto assetPriceDto)
        {
            var validator = _validatorFactory.GetValidator(assetPriceDto);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
            }
            _assetPriceService.Update(assetPriceDto);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Updated" });
        }
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var asset = _assetService.Get(id);

            if (asset == null)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Asset was not found" });
            }

            var vm = new AssetPricesVM()
            {
                Asset = asset,
                AssetPrices = _assetPriceService.GetAssetPrices(id).ToList()
            };
            return PartialView("~/Views/Asset/AssetPrices.cshtml", vm);
        }
    }
}