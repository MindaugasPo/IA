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
                default:
                    return null;
            }
        }
    }
}
