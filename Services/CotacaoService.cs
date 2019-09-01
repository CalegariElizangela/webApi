using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seguradora.API.Controllers.DTO;
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

        public CotacaoResponseDto CalcularCotacao(CotacaoRequestDto request)
        {
            var response = new CotacaoResponseDto();

            request.Idade = DateTime.Today.Year - request.Nascimento.Year;
            if (request.Nascimento.Month > DateTime.Today.Month
                || request.Nascimento.Month == DateTime.Today.Month
                && request.Nascimento.Day > DateTime.Today.Day)
                request.Idade--;

            Validacoes(request);

            var valorTotal = Regras(request, request.Coberturas.Sum(x => int.Parse(x)));

            return response;
        }

        public void Validacoes(CotacaoRequestDto insured)
        {
            if (insured.Idade < 18)
                throw new Exception("Não é permitido contratante menor de 18 anos");

            int.TryParse(insured.Endereco.Cep, out int postalCode);

            if (postalCode.ToString().Length != 8)
                throw new Exception("CEP inválido");

            if (insured.Coberturas.Length == 0)
                throw new Exception("Obrigatório pelo menos uma cobertura");

            if (insured.Coberturas.Length > 4)
                throw new Exception("Máximo de 4 coberturas");
        }

        public decimal Regras(CotacaoRequestDto request, decimal valorTotal)
        {
            if (request.Idade < 30)
            {
                var difference = 30 - request.Idade;
                var addition = difference * 8;
                return valorTotal + addition;  // Acréscimo de X% no valor do seguro
            }
            else if (request.Idade <= 45)
            {
                var difference = 45 - request.Idade;
                var discount = difference * 2;
                return valorTotal - discount;  //Desconto de {0}% no valor do seguro
            }
            else
                return valorTotal; //O Seguro não sofreu alteração de valor
        }
    }
}