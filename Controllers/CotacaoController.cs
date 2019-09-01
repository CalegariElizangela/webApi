using Microsoft.AspNetCore.Mvc;
using Seguradora.API.Domain.Services;
using Seguradora.API.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Seguradora.API.Controllers.DTO;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text;
using System;
using Microsoft.Extensions.Options;

namespace Seguradora.API.Controllers
{
    [Route("/api/[controller]")]
    public class CotacaoController : Controller
    {
        private readonly ICotacaoService _service;

        public CotacaoController(ICotacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Cobertura>> ObterCoberturas()
        {
            var coberturas = await _service.ObterCoberturasAsync();
            return coberturas;
        }

        [HttpPost]
        public async Task<JsonResult> PriceAsync([FromBody] CotacaoRequestDto request)
        {
            try
            {
                request = new CotacaoRequestDto
                {
                    Nome = "Elizangela",
                    Nascimento = new DateTime(1988, 01, 25),
                    Endereco = new Endereco { Cep = "13469111", Bairro = "Vila A", Cidade = "São Paulo", Logradouro = "Rua das Flores" },
                    Coberturas = new string[3] { "4", "6", "7" }
                };

                var response = await _service.CalcularCotacao(request);

                return new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Value = response
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Value = ex.Message
                };
            }
        }
    }
}