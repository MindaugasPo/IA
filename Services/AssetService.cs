using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IADbContext;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public interface IAssetService
    {
        AssetDto Get(Guid id);
        IEnumerable<AssetDto> GetAll();
        AssetDto Create(AssetDto assetDto);
        AssetDto Update(AssetDto assetDto);
        void Delete(Guid id);
        IEnumerable<AssetDto> Search(string searchString);
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

        public AssetDto Create(AssetDto assetDto)
        {
            assetDto.Id = Guid.NewGuid();
            assetDto.CreatedDateUtc = DateTime.UtcNow;
            var asset = _mapper.Map<AssetDto, Asset>(assetDto);
            _context.Assets.Add(asset);
            _context.SaveChanges();
            return assetDto;
        }

        public AssetDto Update(AssetDto assetDto)
        {
            var existingAsset = _context.Assets.SingleOrDefault(x => x.Id == assetDto.Id);
            if (existingAsset != null)
            {
                existingAsset.Title = assetDto.Title;
                existingAsset.Ticker = assetDto.Ticker;
                existingAsset.AssetType = assetDto.AssetType;
                _context.SaveChanges();
                return _mapper.Map<AssetDto>(existingAsset);
            }

            return null;
        }

        public void Delete(Guid id)
        {
            var existingAsset = _context.Assets.SingleOrDefault(x => x.Id == id);
            if (existingAsset != null)
            {
                _context.Assets.Remove(existingAsset);
                _context.SaveChanges();
            }
        }

        public IEnumerable<AssetDto> Search(string searchString)
        {
            return _context.Assets
                .Where(x => x.Ticker.Contains(searchString) || x.Title.Contains(searchString))
                .Select(x => _mapper.Map<AssetDto>(x))
                .ToList();
        }
    }
}
