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
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        public AssetController(
            IAssetService assetService)
        {
            _assetService = assetService;
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
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Update(AssetDto assetDto)
        {
            _assetService.Update(assetDto);
            return new JsonResult(new AjaxResult() { Success = true, Message = "Updated" });
        }

        [HttpPost]
        [ServiceFilter(typeof(IaValidationFilter))]
        public IActionResult Create(AssetDto assetDto)
        {
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
    }
}
