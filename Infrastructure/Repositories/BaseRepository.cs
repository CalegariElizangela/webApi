using Seguradora.API.Infrastructure.Contexts;

namespace Seguradora.API.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}