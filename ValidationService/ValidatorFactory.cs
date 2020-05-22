using Types.DTO;

namespace ValidationService
{
    public interface IAValidatorFactory
    {
        IAValidator GetValidator(object o);
    }

    public class ValidatorFactory : IAValidatorFactory
    {
        public IAValidator GetValidator(object o)
        {
            switch (o)
            {
                case TransactionDto transactionDto:
                    return new TransactionDtoValidator(transactionDto);

                case AssetDto assetDto:
                    return new AssetDtoValidator(assetDto);

                case AssetPriceDto assetPriceDto:
                    return new AssetPriceDtoValidator(assetPriceDto);

                case RegisterDto registerDto:
                    return new RegisterDtoValidator(registerDto);

                case SigninDto signinDto:
                    return new SigninDtoValidator(signinDto);

                case PortfolioDto portfolioDto:
                    return new PortfolioDtoValidator(portfolioDto);

                default:
                    return new UnknownObjectValidator();
            }
        }
    }
}
