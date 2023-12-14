using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }

         [Display(Name="Descrição")]
        public string? Decricao { get; set; }
        public int Quantidade { get; set; }

        [Display(Name="Valor")]
        public decimal Preco { get; set; }
        
        [Display(Name="Nome do Laboratório")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public Marca? Marca { get; set; }
    }
}