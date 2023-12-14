namespace Models
{
    public class PagamentoCheque : TipoDePagamento
    {
        public int PagamentoChequeId { get; set; }
        public int Banco { get; set; }
        public string? NomeBanco { get; set; }
    }
}