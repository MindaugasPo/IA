
using System;

namespace IA.ViewModels
{
    public class PortfolioFormVM
    {
        public Guid PortfolioId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public bool NewPortfolio { get; set; }
    }
}
