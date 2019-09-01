using System;

namespace Seguradora.API.Controllers.DTO
{
    public class CotacaoRequestDto
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public int Idade { get; set; }
        public Endereco Endereco { get; set; }
        public string[] Coberturas { get; set; }
    }

    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
    }
}
