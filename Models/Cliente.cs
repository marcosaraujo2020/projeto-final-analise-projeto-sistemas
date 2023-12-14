namespace Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }
        public ICollection<NotaDeVenda>? NotaDeVendas { get; set; }
    }
}