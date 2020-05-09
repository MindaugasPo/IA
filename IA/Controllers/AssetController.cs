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
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IAssetPriceService _assetPriceService;
        private readonly IAValidatorFactory _validatorFactory;
        public AssetController(
            IAssetService assetService,
            IAValidatorFactory validatorFactory,
            IAssetPriceService assetPriceService)
        {
            _assetService = assetService;
            _validatorFactory = validatorFactory;
            _assetPriceService = assetPriceService;
        }

        [HttpGet]
        public IActionResult EditAssetPrice(Guid id)
        {
            var assetPrice = _assetPriceService.Get(id);
            if (assetPrice == null)
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = "Asset price was not found" });
            }
            var vm = new AssetPriceFormVM() { AssetPriceDto = assetPrice, AssetId = assetPrice.Asset.Id};
            return PartialView("~/Views/Asset/AssetPriceForm.cshtml", vm);
        }
        [HttpGet]
        public IActionResult CreateAssetPrice(Guid assetId)
        {
            var vm = new AssetPriceFormVM() { AssetPriceDto = null, AssetId = assetId};
            return PartialView("~/Views/Asset/AssetPriceForm.cshtml", vm);
        }

        [HttpPost]
        public IActionResult UpdateAssetPrice(AssetPriceDto assetPriceDto)
        {
            var validator = _validatorFactory.GetValidator(assetPriceDto);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
            }
            _assetPriceService.Update(assetPriceDto);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Updated" });
        }
        [HttpPost]
        public IActionResult CreateAssetPrice(AssetPriceDto assetPriceDto)
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
            var asset = _assetService.Get(id);
            if (asset == null)
            {
                return new JsonResult(new AjaxResult(){ Success = false, Message = "Asset was not found" });
            }

            var vm = new AssetFormVM() {Asset = asset};
            return PartialView("~/Views/Asset/AssetForm.cshtml", vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("~/Views/Asset/AssetForm.cshtml", new AssetFormVM());
        }

        [HttpPost]
        public IActionResult Update(AssetDto assetDto)
        {
            var validator = _validatorFactory.GetValidator(assetDto);
            if (!validator.IsValid())
            {
                return new JsonResult(new AjaxResult() { Success = false, Message = validator.Errors() });
            }

            _assetService.Update(assetDto);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Updated" });
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
            return new JsonResult(new AjaxResult(){ Success = true, Message = "Created" });
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
                AssetPrices = _assetPriceService.GetAssetPrices(id).ToList()
            };
            return PartialView("~/Views/Asset/AssetPrices.cshtml", vm);
        }
    }
}
