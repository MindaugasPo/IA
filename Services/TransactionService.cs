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
    }
}
