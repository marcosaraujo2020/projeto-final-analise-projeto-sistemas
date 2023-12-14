using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotaDeVendaId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public int Percentual { get; set; }
        public int Quantidade { get; set; }

        [ForeignKey("NotaDeVendaId")]
        public NotaDeVenda? NotaDeVenda { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto? Produto { get; set; }
    }
}