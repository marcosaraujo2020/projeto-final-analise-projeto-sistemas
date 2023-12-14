namespace Models
{
    public class Pagamento
    {
        public int PagamentoId { get; set; }
        public DateOnly DataLimite { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
    }
}