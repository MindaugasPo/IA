using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Business;
using IADbContext;
using Microsoft.EntityFrameworkCore;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public interface IPortfolioService
    {
        PortfolioDto Get(Guid id);
        PortfolioDto GetCurrent(Guid id);
    }
    public class PortfolioService : BaseService, IPortfolioService
    {
        private readonly IPortfolioBusiness _portfolioBusiness;
        public PortfolioService(
            IAContext context,
            IMapper mapper,
            IPortfolioBusiness portfolioBusiness)
            : base(context, mapper)
        {
            _portfolioBusiness = portfolioBusiness;
        }

        private Portfolio GetPortfolioWithAllTransactions(Guid id)
        {
            return _context.Portfolios
                .Include(x => x.Transactions)
                .ThenInclude(x => x.Asset)
                .SingleOrDefault(x => x.Id == id);
        }

        public PortfolioDto Get(Guid id)
        {
            var portfolio = GetPortfolioWithAllTransactions(id);
            return _mapper.Map<Portfolio, PortfolioDto>(portfolio);
        }

        public PortfolioDto GetCurrent(Guid id)
        {
            var portfolio = GetPortfolioWithAllTransactions(id);
            if (portfolio == null)
            {
                return null;
            }
            var currentPortfolio = _portfolioBusiness.GetCurrentPortfolio(portfolio);
            return _mapper.Map<Portfolio, PortfolioDto>(currentPortfolio);
        }
    }
}
