using AutoMapper;
using IADbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public interface IAssetPriceService
    {
        IEnumerable<AssetPriceDto> GetAssetPrices(Guid id);
        AssetPriceDto Get(Guid id);
        void Update(AssetPriceDto assetPriceDto);
        void Create(AssetPriceDto assetPriceDto);
        void Delete(Guid id);
    }
    public class AssetPriceService : BaseService, IAssetPriceService
    {
        public AssetPriceService(
            IAContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public void Delete(Guid id)
        {
            var asset = _context.AssetPrices.SingleOrDefault(x => x.Id == id);
            if (asset != null)
            {
                _context.AssetPrices.Remove(asset);
                _context.SaveChanges();
            }
        }

        public IEnumerable<AssetPriceDto> GetAssetPrices(Guid id)
        {
            var prices = _context.AssetPrices.Where(x => x.AssetId == id);
            var dtos = prices.Select(x => _mapper.Map<AssetPrice, AssetPriceDto>(x));
            return dtos;
        }

        public AssetPriceDto Get(Guid id)
        {
            var assetPrice = _context.AssetPrices
                .Include(x => x.Asset)
                .SingleOrDefault(x => x.Id == id);
            if (assetPrice == null)
            {
                return null;
            }

            return _mapper.Map<AssetPrice, AssetPriceDto>(assetPrice);
        }

        public void Update(AssetPriceDto assetPriceDto)
        {
            var existingAssetPrice = _context.AssetPrices.SingleOrDefault(x => x.Id == assetPriceDto.Id);
            if (existingAssetPrice != null)
            {
                UpdateExisting(existingAssetPrice, assetPriceDto);
                _context.SaveChanges();
            }
        }

        public void Create(AssetPriceDto assetPriceDto)
        {
            var existingAsset = _context.AssetPrices.SingleOrDefault(x =>
                x.AssetId == assetPriceDto.AssetId && x.Date == assetPriceDto.Date);

            if (existingAsset != null)
            {
                UpdateExisting(existingAsset, assetPriceDto);
            }
            else
            {
                var asset = _mapper.Map<AssetPriceDto, AssetPrice>(assetPriceDto);
                _context.AssetPrices.Add(asset);
            }
            _context.SaveChanges();
        }

        private void UpdateExisting(AssetPrice existingPrice, AssetPriceDto newPrice)
        {
            existingPrice.Date = newPrice.Date;
            existingPrice.OpenPrice = newPrice.OpenPrice;
            existingPrice.HighPrice = newPrice.HighPrice;
            existingPrice.LowPrice = newPrice.LowPrice;
            existingPrice.ClosePrice = newPrice.ClosePrice;
        }
    }
}
