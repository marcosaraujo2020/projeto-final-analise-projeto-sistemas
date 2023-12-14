namespace Models
{
    public class PagamentoCartao : TipoDePagamento
    {
        public int PagamentoCartaoId { get; set; }
        public string? NumeroCartao { get; set; }
        public string? Bandeira { get; set; }
    }
}