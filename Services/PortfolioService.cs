﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        PortfolioDto GetHistoricPortfolio(Guid id);
        IEnumerable<PortfolioDto> GetAll(string userId);
        PortfolioDto Create(PortfolioDto portfolio);
        void Update(PortfolioDto portfolioDto);
        void Delete(Guid id);
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
            var portfolioDto = _mapper.Map<Portfolio, PortfolioDto>(portfolio);
            var currentPortfolio = _portfolioBusiness.GetCurrentPortfolio(portfolioDto);
            return currentPortfolio;
        }

        public PortfolioDto GetHistoricPortfolio(Guid id)
        {
            var portfolio = GetPortfolioWithAllTransactions(id);
            if (portfolio == null)
            {
                return null;
            }

            var portfolioDto = _mapper.Map<Portfolio, PortfolioDto>(portfolio);
            var historicPortfolio = _portfolioBusiness.GetHistoricPortfolio(portfolioDto);
            return historicPortfolio;
        }

        public IEnumerable<PortfolioDto> GetAll(string userId)
        {
            return _context.Portfolios
                .Where(x => x.UserId == userId)
                .Select(x => _mapper.Map<Portfolio, PortfolioDto>(x)).ToList();
        }

        public PortfolioDto Create(PortfolioDto portfolioDto)
        {
            var portfolio = _mapper.Map<PortfolioDto, Portfolio>(portfolioDto);
            portfolio.Id = Guid.NewGuid();
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
            return _mapper.Map<Portfolio, PortfolioDto>(portfolio);
        }

        public void Update(PortfolioDto portfolioDto)
        {
            var portfolio = _context.Portfolios.SingleOrDefault(x => x.Id == portfolioDto.Id);
            if (portfolio == null)
            {
                return;
            }

            portfolio.Title = portfolioDto.Title;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var portfolio = _context.Portfolios
                .Include(x => x.Transactions)
                .SingleOrDefault(x => x.Id == id);

            if (portfolio == null)
            {
                return;
            }

            foreach (var portfolioTransaction in portfolio.Transactions)
            {
                _context.Transactions.Remove(portfolioTransaction);
            }

            _context.Portfolios.Remove(portfolio);
            _context.SaveChanges();
        }
    }
}
