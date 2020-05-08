using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AutoMapper;
using IADbContext;
using Microsoft.EntityFrameworkCore;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public interface IAssetService
    {
        AssetDto Get(Guid id);
        IEnumerable<AssetDto> GetAll();
        IEnumerable<AssetPriceDto> GetAssetPrices(Guid id);
        void Create(AssetDto assetDto);
    }
    public class AssetService : BaseService, IAssetService
    { 
        public AssetService(
            IAContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }
        public AssetDto Get(Guid id)
        {
            var asset = _context.Assets.SingleOrDefault(x => x.Id == id);
            AssetDto assetDto = null;

            if (asset != null)
            {
                assetDto =_mapper.Map<Asset, AssetDto>(asset);
            }

            return assetDto;
        }

        public IEnumerable<AssetDto> GetAll()
        {
            return _context.Assets.Select(x => _mapper.Map<Asset, AssetDto>(x)).ToList();
        }

        public IEnumerable<AssetPriceDto> GetAssetPrices(Guid id)
        {
            var prices = _context.AssetPrices.Where(x => x.AssetId == id);
            var dtos = prices.Select(x => _mapper.Map<AssetPrice, AssetPriceDto>(x));
            return dtos;
        }

        public void Create(AssetDto assetDto)
        {
            var asset = _mapper.Map<AssetDto, Asset>(assetDto);
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }
    }
}
