using System.Linq;
using Types.DTO;
using Types.Entities;

namespace Business
{
    public interface IPortfolioBusiness
    {
        PortfolioDto GetCurrentPortfolio(PortfolioDto portfolio);
        PortfolioDto GetHistoricPortfolio(PortfolioDto portfolio);
    }
    public class PortfolioBusiness : IPortfolioBusiness
    {
        public PortfolioDto GetCurrentPortfolio(PortfolioDto portfolio)
        {
            portfolio.Transactions = portfolio.Transactions.Where(x => x.CloseDateUtc == null).ToList();
            return portfolio;
        }

        public PortfolioDto GetHistoricPortfolio(PortfolioDto portfolio)
        {
            portfolio.Transactions = portfolio.Transactions.Where(x => x.CloseDateUtc != null).ToList();
            return portfolio;
        }
    }
}
