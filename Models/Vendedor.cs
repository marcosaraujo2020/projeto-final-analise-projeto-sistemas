namespace Models
{
    public class Vendedor
    {
        public int VendedorId { get; set; }
        public string? Nome { get; set; }
        public ICollection<NotaDeVenda>? NotaDeVendas { get; set; }
    }
}