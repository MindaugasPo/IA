using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Types.DTO;

namespace IA.ViewModels
{
    public class NewTransactionVM
    {
        public Guid PortfolioId { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}
