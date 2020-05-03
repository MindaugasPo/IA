using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Types.Entities;

namespace Business
{
    public interface IPortfolioBusiness
    {
        Portfolio GetCurrentPortfolio(Portfolio portfolio);
    }
    public class PortfolioBusiness : IPortfolioBusiness
    {
        public Portfolio GetCurrentPortfolio(Portfolio portfolio)
        {
            portfolio.Transactions = portfolio.Transactions.Where(x => x.CloseDateUtc == null).ToList();
            return portfolio;
        }
    }
}
