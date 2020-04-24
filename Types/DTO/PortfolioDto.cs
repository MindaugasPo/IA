using System;
using System.Collections.Generic;
using System.Text;

namespace Types.DTO
{
    public class PortfolioDto : BaseDto
    {
        public string Title { get; set; }
        public List<TransactionDto> Transactions { get; set; }

    }
}
