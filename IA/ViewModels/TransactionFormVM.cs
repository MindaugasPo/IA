using System;
using System.Collections.Generic;
using Types.DTO;

namespace IA.ViewModels
{
    public class TransactionFormVM
    {
        public Guid PortfolioId { get; set; }
        public List<PortfolioDto> Portfolios { get; set; }
        public List<AssetDto> Assets { get; set; }
        public TransactionDto Transaction { get; set; }
    }
}
