using AutoMapper;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<Asset, AssetDto>();
            CreateMap<AssetDto, Asset>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Portfolio, PortfolioDto>();
            CreateMap<PortfolioDto, Portfolio>();
            CreateMap<AssetPrice, AssetPriceDto>();
            CreateMap<AssetPriceDto, AssetPrice>();
        }
    }
}
