﻿using System;
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
        void Create(AssetDto assetDto);
        void Update(AssetDto assetDto);
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

        public void Create(AssetDto assetDto)
        {
            var asset = _mapper.Map<AssetDto, Asset>(assetDto);
            _context.Assets.Add(asset);
            _context.SaveChanges();
        }

        public void Update(AssetDto assetDto)
        {
            var newAsset = _mapper.Map<AssetDto, Asset>(assetDto);
            var existingAsset = _context.Assets.SingleOrDefault(x => x.Id == assetDto.Id);
            if (existingAsset != null)
            {
                existingAsset.Title = newAsset.Title;
                existingAsset.Ticker = newAsset.Ticker;
                existingAsset.AssetType = newAsset.AssetType;
            }

            _context.SaveChanges();
        }
    }
}
