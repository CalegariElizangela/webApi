using System.Collections.Generic;
using System.Threading.Tasks;
using Seguradora.API.Domain.Models;
using Seguradora.API.Domain.Repositories;
using Seguradora.API.Domain.Services;

namespace Seguradora.API.Services
{
    public class CotacaoService : ICotacaoService
    {
        private readonly ICoberturaRepository _repository;

        public CotacaoService(ICoberturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Cobertura>> ObterCoberturasAsync()
        {
            return await _repository.ObterCoberturasAsync();
        }
    }
}