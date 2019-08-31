using Microsoft.AspNetCore.Mvc;
using Seguradora.API.Domain.Services;
using Seguradora.API.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Seguradora.API.Controllers.DTO;

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
        public async Task<JsonResult> ObterCoberturas([FromBody] CotacaoRequestDto request)
        {
            var coberturas = request;
            return new JsonResult(coberturas);
        }
    }
}