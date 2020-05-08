using System;
using System.Collections.Generic;
using Types.DTO;

namespace IA.ViewModels
{
    public class PortfolioVM
    {
        public Guid PortfolioId { get; set; }
        public string Title { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
