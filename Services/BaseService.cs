using AutoMapper;
using IADbContext;

namespace Services
{
    public abstract class BaseService
    {
        protected readonly IAContext _context;
        protected readonly IMapper _mapper;
        protected BaseService(
            IAContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
