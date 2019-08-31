using System.Collections.Generic;
using System.Threading.Tasks;
using Seguradora.API.Domain.Models;

namespace Seguradora.API.Domain.Services
{
    public interface ICoberturaService
    {
        Task<IEnumerable<Cobertura>> ObterCoberturasAsync();
    }
}