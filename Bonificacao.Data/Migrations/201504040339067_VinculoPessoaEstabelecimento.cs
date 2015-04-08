namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VinculoPessoaEstabelecimento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "EstabelecimentoId", c => c.Int());
            CreateIndex("dbo.Pessoa", "EstabelecimentoId");
            AddForeignKey("dbo.Pessoa", "EstabelecimentoId", "dbo.Estabelecimento", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "EstabelecimentoId", "dbo.Estabelecimento");
            DropIndex("dbo.Pessoa", new[] { "EstabelecimentoId" });
            DropColumn("dbo.Pessoa", "EstabelecimentoId");
        }
    }
}
