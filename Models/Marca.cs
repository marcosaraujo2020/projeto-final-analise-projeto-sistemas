using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }
        public string? Nome { get; set; }
        public string? Decricao { get; set; }

        public ICollection<Produto>? Produtos { get; set; }
    }
}