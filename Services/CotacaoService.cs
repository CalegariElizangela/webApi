using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Seguradora.API.Controllers.DTO;
using Seguradora.API.Domain.Models;
using Seguradora.API.Domain.Repositories;
using Seguradora.API.Domain.Services;
using Seguradora.API.Infrastructure.Configuracao;

namespace Seguradora.API.Services
{
    public class CotacaoService : ICotacaoService
    {
        private readonly ICoberturaRepository _repository;
        private readonly AppSettings _settings;
        static HttpClient client = new HttpClient();

        public CotacaoService(ICoberturaRepository repository, IOptions<AppSettings> settings)
        {
            _repository = repository;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<Cobertura>> ObterCoberturasAsync()
        {
            return await _repository.ObterCoberturasAsync();
        }

        public async Task<CotacaoResponseDto> CalcularCotacao(CotacaoRequestDto request)
        {
            var idade = CalcularIdade(DateTime.Parse(request.Nascimento));

            await ValidarRequest(request, idade);

            decimal valorTotalCoberturas = SomarCoberturas(request, idade);
            decimal valorPremio = CalcularDesconto(idade, valorTotalCoberturas);
            var parcelas = ObterParcelamento(valorPremio);

            return new CotacaoResponseDto
            {
                Valor_Parcelas = valorPremio / parcelas,
                Cobertura_Total = valorTotalCoberturas,
                Primeiro_Vencimento = ObterProximoQuintoDiaUtil().ToString(),
                Parcelas = parcelas,
                Premio = valorPremio
            };
        }

        private DateTime ObterProximoQuintoDiaUtil()
        {
            var hoje = DateTime.Now;
            var proximoMes = hoje.AddMonths(1);
            proximoMes = proximoMes.AddDays(5);

            while (proximoMes.DayOfWeek == DayOfWeek.Saturday || proximoMes.DayOfWeek == DayOfWeek.Sunday)
            {
                proximoMes = proximoMes.AddDays(1);
            }

            return new DateTime(proximoMes.Year, proximoMes.Month, proximoMes.Day).Date;
        }

        private int ObterParcelamento(decimal valorTotal)
        {
            if (valorTotal <= 500)
                return 1;
            else if (valorTotal <= 1000)
                return 2;
            else if (valorTotal <= 2000)
                return 3;
            else
                return 4;
        }

        private decimal SomarCoberturas(CotacaoRequestDto request, int idade)
        {
            decimal valorTotalCoberturas = 0;

            foreach (var cobertura in request.Coberturas)
            {
                valorTotalCoberturas += _repository.ObterCoberturaAsync(int.Parse(cobertura)).Result.Valor;
            }

            return valorTotalCoberturas;
        }

        private int CalcularIdade(DateTime nascimento)
        {
            var idade = DateTime.Today.Year - nascimento.Year;
            if (nascimento.Month > DateTime.Today.Month
                || nascimento.Month == DateTime.Today.Month
                && nascimento.Day > DateTime.Today.Day)
                idade--;

            return idade;
        }

        public async Task ValidarRequest(CotacaoRequestDto request, int idade)
        {
            if (idade < 18)
                throw new Exception("Não é permitido contratante menor de 18 anos");

            Regex rgx = new Regex("^\\d{5}[-]\\d{3}$");
            if (!rgx.IsMatch(request.Endereco.Cep))
                throw new Exception("CEP inválido");

            if (request.Coberturas.Length == 0)
                throw new Exception("Obrigatório pelo menos uma cobertura");

            if (request.Coberturas.Length > 4)
                throw new Exception("Máximo de 4 coberturas");

            if (await VerificarCidadeNaoAtendida(request.Endereco.Cidade))
                throw new Exception("Cidade não atendida");
        }

        private async Task<bool> VerificarCidadeNaoAtendida(string cidade)
        {
            HttpResponseMessage response = await client.GetAsync(_settings.Cidades);
            if (response.Content.ReadAsStringAsync().Result.Contains(cidade))
                return false;
            else
                return true;
        }

        public decimal CalcularDesconto(int idade, decimal valorTotalCoberturas)
        {
            if (idade < 30)
            {
                var valorAcrescimo = decimal.Divide(((30 - idade) * 8), 100);
                return valorTotalCoberturas + (valorTotalCoberturas - valorAcrescimo);  // Acréscimo de X% no valor do seguro
            }
            else if (idade <= 45)
            {
                var valorDesconto = decimal.Divide(((45 - idade) * 2), 100);
                return valorTotalCoberturas - (valorTotalCoberturas * valorDesconto);  //Desconto de {0}% no valor do seguro
            }
            else
                return valorTotalCoberturas; //O Seguro não sofreu alteração de valor
        }
    }
}