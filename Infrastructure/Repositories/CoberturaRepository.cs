
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seguradora.API.Domain.Models;
using Seguradora.API.Domain.Repositories;
using Seguradora.API.Infrastructure.Contexts;
using Seguradora.API.Infrastructure.Repositories;

namespace Seguradora.API.Persistence.Repositories
{
    public class CoberturaRepository : BaseRepository, ICoberturaRepository
    {
        public CoberturaRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Cobertura>> ObterCoberturasAsync()
        {
            return await _context.Coberturas.ToListAsync();
        }

        public async Task<Cobertura> ObterCoberturaAsync(int idCobertura)
        {
            return await _context.Coberturas.FindAsync(idCobertura);
        }
    }
}