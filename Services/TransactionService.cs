using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using IADbContext;
using Microsoft.EntityFrameworkCore;
using Types.DTO;
using Types.Entities;

namespace Services
{
    public interface ITransactionService
    {
        IEnumerable<TransactionDto> GetAll();
        void Create(TransactionDto transactionDto);
        void Close(Guid id, decimal closePrice);
        void Delete(Guid id);
    }
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(
            IAContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<TransactionDto> GetAll()
        {
            return _context.Transactions
                .Include(x => x.Asset)
                .Select(x => _mapper.Map<Transaction, TransactionDto>(x));
        }

        public void Create(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Close(Guid id, decimal closePrice)
        {
            var transaction = _context.Transactions.SingleOrDefault(x => x.Id == id);
            if (transaction != null)
            {
                transaction.CloseDateUtc = DateTime.UtcNow;
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
    }
}
