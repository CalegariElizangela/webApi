using Newtonsoft.Json;
using System;

namespace Seguradora.API.Controllers.DTO
{
    public class CotacaoRequestDto
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("nascimento")]
        public string Nascimento { get; set; }

        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        [JsonProperty("coberturas")]
        public string[] Coberturas { get; set; }
    }

    public class Endereco
    {
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("cidade")]
        public string Cidade { get; set; }
    }
}
