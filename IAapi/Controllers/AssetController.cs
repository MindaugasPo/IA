using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Services;
using Types.DTO;

namespace IAapi.Controllers
{
    [Route("apiv1/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(
            IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public IEnumerable<AssetDto> Get()
        {
            return _assetService.GetAll();
        }

        [HttpPost]
        public AssetDto Post(AssetDto newAsset)
        {
            var createdAsset = _assetService.Create(newAsset);
            return createdAsset;
        }

        [HttpDelete]
        public void Delete(Guid id)
        {
            _assetService.Delete(id);
        }
    }
}