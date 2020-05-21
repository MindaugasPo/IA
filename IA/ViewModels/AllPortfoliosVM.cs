using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IA.ViewModels
{
    public class AllPortfoliosVM
    {
        public List<PortfolioVM> AllPortfolios { get; set; }
        public PortfolioVM SelectedPortfolio { get; set; }
    }
}
