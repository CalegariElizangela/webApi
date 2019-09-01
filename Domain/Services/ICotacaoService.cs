using System.Collections.Generic;
using System.Threading.Tasks;
using Seguradora.API.Controllers.DTO;
using Seguradora.API.Domain.Models;

namespace Seguradora.API.Domain.Services
{
    public interface ICotacaoService
    {
        Task<IEnumerable<Cobertura>> ObterCoberturasAsync();
        CotacaoResponseDto CalcularCotacao(CotacaoRequestDto request);
    }
}