
using System.Collections.Generic;
using System.Threading.Tasks;
using Seguradora.API.Domain.Models;

namespace Seguradora.API.Domain.Repositories
{
    public interface ICoberturaRepository
    {
        Task<IEnumerable<Cobertura>> ObterCoberturasAsync();
    }
}