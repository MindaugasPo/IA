using System.Collections.Generic;

namespace Types.DTO
{
    public class PortfolioDto : BaseDto
    {
        public string Title { get; set; }
        public List<TransactionDto> Transactions { get; set; }

    }
}
