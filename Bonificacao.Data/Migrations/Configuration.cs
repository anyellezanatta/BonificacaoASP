namespace Bonificacao.Data.Migrations
{
    using Bonificacao.Data.Utils;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Bonificacao.Data.BonificacaoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bonificacao.Data.BonificacaoContext context)
        {
            context.Pessoas.AddOrUpdate(
              p => p.Usuario,
              new Pessoa() { Usuario = "anyelle.ad@gmail.com", Nome = "Anyelle", Senha = SHA256Generator.GetHash("123456@"), Tipo = TipoPessoa.Administrador }
            );

        }
    }
}
