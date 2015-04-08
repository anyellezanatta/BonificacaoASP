namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Indicacao");
            AddPrimaryKey("dbo.Indicacao", new[] { "EmailDestino", "PessoaId", "EstabelecimentoId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Indicacao");
            AddPrimaryKey("dbo.Indicacao", new[] { "EmailDestino", "PessoaId" });
        }
    }
}
