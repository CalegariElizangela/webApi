
namespace Seguradora.API.Domain.Models
{
    public class Cobertura
    {
        public int Id { get; set; }         
        public string Nome { get; set; }
        public int Premio { get; set; }        
        public decimal Valor { get; set; }
        public bool Obrigatorio { get; set; }
    }
}