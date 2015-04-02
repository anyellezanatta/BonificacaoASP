namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailPessoaChave : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Indicacao");
            AlterColumn("dbo.Indicacao", "EmailDestino", c => c.String(nullable: false, maxLength: 60));
            AddPrimaryKey("dbo.Indicacao", new[] { "EmailDestino", "PessoaId" });
            DropColumn("dbo.Indicacao", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicacao", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Indicacao");
            AlterColumn("dbo.Indicacao", "EmailDestino", c => c.String());
            AddPrimaryKey("dbo.Indicacao", "Id");
        }
    }
}
