using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonificacao.Data
{
    public class BonificacaoContext : DbContext
    {
        public BonificacaoContext(): base("DefaultConnection")
        {

        }


        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<GrupoEstabelecimento> GruposEstabelecimento { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Indicacao> Indicacoes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Indicacao>().HasRequired(e => e.PessoaOrigem).WithMany(e => e.IndicadoPor).HasForeignKey(e => e.PessoaOrigemId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Indicacao>().HasRequired(e => e.PessoaIndicada).WithMany(e => e.Indicacoes).HasForeignKey(e => e.PessoaIndicadaId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Indicacao>().HasKey(e => new { e.PessoaOrigemId, e.PessoaIndicadaId });

            modelBuilder.Entity<Movimento>().HasRequired(e => e.Cliente).WithMany(e => e.Movimentos).HasForeignKey(e => e.ClienteId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Movimento>().HasRequired(e => e.Estabelecimento).WithMany(e => e.Movimentos).HasForeignKey(e => e.EstabelecimentoId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Movimento>().HasRequired(e => e.Frentista).WithMany(e => e.Movimentos).HasForeignKey(e => e.FrentistaId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Movimento>().HasRequired(e => e.Produto).WithMany(e => e.Movimentos).HasForeignKey(e => e.ProdutoId).WillCascadeOnDelete(false);
        }
    }
}
