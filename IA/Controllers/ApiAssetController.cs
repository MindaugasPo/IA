using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Services;
using Types.DTO;

namespace IA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public ApiAssetController(
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
