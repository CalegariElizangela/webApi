using Microsoft.AspNetCore.Mvc;
using Seguradora.API.Domain.Services;
using Seguradora.API.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Seguradora.API.Controllers
{
    [Route("/api/[controller]")]
    public class CotacaoController : Controller
    {
        private readonly ICoberturaService _service;
        public CotacaoController(ICoberturaService coberturaService)
        {
            _service = coberturaService; 
        }

        [HttpGet]
        public async Task<IEnumerable<Cobertura>> ObterCoberturas()
        {
            var coberturas = await _service.ObterCoberturasAsync();
            return coberturas;
        }
    }
}