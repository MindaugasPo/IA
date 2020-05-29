using System.Collections.Generic;
using Types.Entities;

namespace Types.DTO
{
    public class PortfolioDto : BaseDto
    {
        public string Title { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
