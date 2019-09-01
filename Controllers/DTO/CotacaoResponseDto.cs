namespace Seguradora.API.Controllers.DTO
{
    public class CotacaoResponseDto
    {
        public decimal Premio { get; set; }
        public int Parcelas { get; set; }
        public decimal Valor_Parcelas { get; set; }
        public string Primeiro_Vencimento { get; set; }
        public decimal Cobertura_Total { get; set; }
    }
}
