using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class NotaDeVenda
    {
        [Key]
        [Display(Name="Nº da Nota")]
        public int NotaDeVendaId { get; set; }

        [Display(Name="Data / Hora")]
        public DateTime Data { get; set; }
        public bool Tipo { get; set; }

        [Display(Name="Vendedor")]
        public int VendedorId { get; set; }
        
        [ForeignKey("VendedorId")]
        public Vendedor? Vendedor { get; set; }

        [Display(Name="Cliente")]
        public int ClienteId { get; set; }
        
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Display(Name="Pagamento")]
        public int PagamentoId { get; set; }
        public Pagamento? Pagamento { get; set; }

        [Display(Name="Transportadora")]
        public int TransportadoraId { get; set; }
        public Transportadora? Transportadora { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}