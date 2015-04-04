namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndicacaoEstabelecimento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicacao", "EstabelecimentoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Indicacao", "EstabelecimentoId");
            AddForeignKey("dbo.Indicacao", "EstabelecimentoId", "dbo.Estabelecimento", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indicacao", "EstabelecimentoId", "dbo.Estabelecimento");
            DropIndex("dbo.Indicacao", new[] { "EstabelecimentoId" });
            DropColumn("dbo.Indicacao", "EstabelecimentoId");
        }
    }
}
