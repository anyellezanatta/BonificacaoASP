namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configuracao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BonusPorLitro = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NivelBonificacao = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estabelecimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        GrupoEstabelecimentoId = c.Int(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrupoEstabelecimento", t => t.GrupoEstabelecimentoId)
                .Index(t => t.GrupoEstabelecimentoId);
            
            CreateTable(
                "dbo.GrupoEstabelecimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoMovimento = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        EstabelecimentoId = c.Int(nullable: false),
                        FrentistaId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Debito = c.Decimal(precision: 18, scale: 2),
                        Credito = c.Decimal(precision: 18, scale: 2),
                        ValorPago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataHoraMovimento = c.DateTime(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.ClienteId)
                .ForeignKey("dbo.Estabelecimento", t => t.EstabelecimentoId)
                .ForeignKey("dbo.Pessoa", t => t.FrentistaId, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .Index(t => t.ClienteId)
                .Index(t => t.EstabelecimentoId)
                .Index(t => t.FrentistaId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.Int(nullable: false),
                        Nome = c.String(),
                        Usuario = c.String(),
                        Senha = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indicacao",
                c => new
                    {
                        PessoaOrigemId = c.Int(nullable: false),
                        PessoaIndicadaId = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.PessoaOrigemId, t.PessoaIndicadaId })
                .ForeignKey("dbo.Pessoa", t => t.PessoaIndicadaId)
                .ForeignKey("dbo.Pessoa", t => t.PessoaOrigemId)
                .Index(t => t.PessoaOrigemId)
                .Index(t => t.PessoaIndicadaId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCriacao = c.DateTime(nullable: false),
                        DataModificacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimento", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Movimento", "FrentistaId", "dbo.Pessoa");
            DropForeignKey("dbo.Movimento", "EstabelecimentoId", "dbo.Estabelecimento");
            DropForeignKey("dbo.Movimento", "ClienteId", "dbo.Pessoa");
            DropForeignKey("dbo.Indicacao", "PessoaOrigemId", "dbo.Pessoa");
            DropForeignKey("dbo.Indicacao", "PessoaIndicadaId", "dbo.Pessoa");
            DropForeignKey("dbo.Estabelecimento", "GrupoEstabelecimentoId", "dbo.GrupoEstabelecimento");
            DropIndex("dbo.Indicacao", new[] { "PessoaIndicadaId" });
            DropIndex("dbo.Indicacao", new[] { "PessoaOrigemId" });
            DropIndex("dbo.Movimento", new[] { "ProdutoId" });
            DropIndex("dbo.Movimento", new[] { "FrentistaId" });
            DropIndex("dbo.Movimento", new[] { "EstabelecimentoId" });
            DropIndex("dbo.Movimento", new[] { "ClienteId" });
            DropIndex("dbo.Estabelecimento", new[] { "GrupoEstabelecimentoId" });
            DropTable("dbo.Produto");
            DropTable("dbo.Indicacao");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Movimento");
            DropTable("dbo.GrupoEstabelecimento");
            DropTable("dbo.Estabelecimento");
            DropTable("dbo.Configuracao");
        }
    }
}
