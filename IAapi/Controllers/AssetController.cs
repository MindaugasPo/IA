using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using IAapi.Filters;
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
        [Route("create")]
        [ServiceFilter(typeof(IaValidationFilter))]
        public AssetDto Create(AssetDto newAsset)
        {
            var createdAsset = _assetService.Create(newAsset);
            return createdAsset;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(Guid id)
        {
            _assetService.Delete(id);
        }

        [HttpGet]
        [Route("search/{searchString}")]
        public IEnumerable<AssetDto> Search(string searchString)
        {
            return _assetService.Search(searchString);
        }

        [HttpPost]
        [Route("Update")]
        public AssetDto Update(AssetDto assetDto)
        {
            return _assetService.Update(assetDto);
        }
    }
}