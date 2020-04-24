using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Types.DTO;

namespace IA.ViewModels
{
    public class PortfolioVM
    {
        public string Title { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
