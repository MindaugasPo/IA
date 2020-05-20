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
    public class AssetPriceController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IAssetPriceService _assetPriceService;

        public AssetPriceController(
            IAssetService assetService,
            IAssetPriceService assetPriceService)
        {
            _assetService = assetService;
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
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Create(AssetPriceDto assetPriceDto)
        {
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
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Update(AssetPriceDto assetPriceDto)
        {
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