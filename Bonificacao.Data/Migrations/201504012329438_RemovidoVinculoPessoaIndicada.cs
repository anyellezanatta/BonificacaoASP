namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovidoVinculoPessoaIndicada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Indicacao", "PessoaOrigemId", "dbo.Pessoa");
            DropIndex("dbo.Indicacao", new[] { "PessoaOrigemId" });
            RenameColumn(table: "dbo.Indicacao", name: "PessoaIndicadaId", newName: "PessoaId");
            RenameIndex(table: "dbo.Indicacao", name: "IX_PessoaIndicadaId", newName: "IX_PessoaId");
            DropPrimaryKey("dbo.Indicacao");
            AddColumn("dbo.Indicacao", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Indicacao", "EmailDestino", c => c.String());
            AddPrimaryKey("dbo.Indicacao", "Id");
            DropColumn("dbo.Indicacao", "PessoaOrigemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indicacao", "PessoaOrigemId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Indicacao");
            DropColumn("dbo.Indicacao", "EmailDestino");
            DropColumn("dbo.Indicacao", "Id");
            AddPrimaryKey("dbo.Indicacao", new[] { "PessoaOrigemId", "PessoaIndicadaId" });
            RenameIndex(table: "dbo.Indicacao", name: "IX_PessoaId", newName: "IX_PessoaIndicadaId");
            RenameColumn(table: "dbo.Indicacao", name: "PessoaId", newName: "PessoaIndicadaId");
            CreateIndex("dbo.Indicacao", "PessoaOrigemId");
            AddForeignKey("dbo.Indicacao", "PessoaOrigemId", "dbo.Pessoa", "Id");
        }
    }
}
