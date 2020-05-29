using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IADbContext;
using Microsoft.EntityFrameworkCore;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public interface ITransactionService
    {
        IEnumerable<TransactionDto> GetAll(string userId);
        void Create(TransactionDto transactionDto);
        void Update(TransactionDto transactionDto);
        void Close(Guid id, decimal closePrice, DateTime closeDate);
        void Delete(Guid id);
        TransactionDto Get(Guid id);
    }
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(
            IAContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<TransactionDto> GetAll(string userId)
        {
            return _context.Transactions
                .Include(x => x.Asset)
                .Include(x => x.Portfolio)
                .Where(x => x.Portfolio.UserId == userId)
                .Select(x => _mapper.Map<Transaction, TransactionDto>(x));
        }

        public void Create(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Update(TransactionDto transaction)
        {
            var newTransaction = _mapper.Map<TransactionDto, Transaction>(transaction);
            var existingTransaction = _context.Transactions.SingleOrDefault(x => x.Id == transaction.Id);

            if (existingTransaction != null)
            {
                existingTransaction.OpenPrice = newTransaction.OpenPrice;
                existingTransaction.OpenDateUtc = newTransaction.OpenDateUtc;
                existingTransaction.Amount = newTransaction.Amount;
                existingTransaction.Commission = newTransaction.Commission;
                existingTransaction.TransactionType = newTransaction.TransactionType;
                existingTransaction.Currency = newTransaction.Currency;
                existingTransaction.ClosePrice = newTransaction.ClosePrice;
                existingTransaction.CloseDateUtc = newTransaction.CloseDateUtc;
                existingTransaction.AssetId = newTransaction.AssetId;
                existingTransaction.PortfolioId = newTransaction.PortfolioId;
            }

            _context.SaveChanges();
        }

        public void Close(Guid id, decimal closePrice, DateTime closeDate)
        {
            var transaction = _context.Transactions.SingleOrDefault(x => x.Id == id);
            if (transaction != null)
            {
                transaction.CloseDateUtc = closeDate;
                transaction.ClosePrice = closePrice;
            }
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var transaction = _context.Transactions.SingleOrDefault(x => x.Id == id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }

        public TransactionDto Get(Guid id)
        {
            var transaction = _context.Transactions
                .Include(x => x.Portfolio)
                .SingleOrDefault(x => x.Id == id);
            TransactionDto transactionDto = null;

            if (transaction != null)
            {
                transactionDto = _mapper.Map<Transaction, TransactionDto>(transaction);
            }

            return transactionDto;
        }
    }
}
