using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class MyDbVendas : DbContext
    {
        public MyDbVendas(DbContextOptions<MyDbVendas> options) : base(options){}

        public DbSet<Vendedor> Vendedors { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<NotaDeVenda> NotaDeVendas { get; set; }
        public DbSet<PagamentoCartao> PagamentoCartaos { get; set; }
        public DbSet<PagamentoCheque> PagamentoCheques { get; set; }
        public DbSet<Transportadora> Transportadoras { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasKey(it=>new {it.ProdutoId, it.NotaDeVendaId});
        }


    }
}