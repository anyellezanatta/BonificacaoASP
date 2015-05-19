namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estabelecimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        GrupoEstabelecimentoId = c.Int(),
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrupoEstabelecimento", t => t.GrupoEstabelecimentoId)
                .Index(t => t.GrupoEstabelecimentoId);
            
            CreateTable(
                "dbo.GrupoEstabelecimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indicacao",
                c => new
                    {
                        EmailDestino = c.String(nullable: false, maxLength: 60),
                        PessoaId = c.Int(nullable: false),
                        EstabelecimentoId = c.Int(nullable: false),
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => new { t.EmailDestino, t.PessoaId, t.EstabelecimentoId })
                .ForeignKey("dbo.Estabelecimento", t => t.EstabelecimentoId, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.PessoaId)
                .Index(t => t.PessoaId)
                .Index(t => t.EstabelecimentoId);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.Int(nullable: false),
                        Nome = c.String(),
                        Usuario = c.String(maxLength: 60),
                        Senha = c.String(),
                        EstabelecimentoId = c.Int(),
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estabelecimento", t => t.EstabelecimentoId)
                .Index(t => t.Usuario, unique: true)
                .Index(t => t.EstabelecimentoId);
            
            CreateTable(
                "dbo.Movimento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoMovimento = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        EstabelecimentoId = c.Int(nullable: false),
                        VendedorId = c.Int(nullable: false),
                        ProdutoId = c.Int(nullable: false),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorBonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaldoBonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorPago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pessoa", t => t.ClienteId)
                .ForeignKey("dbo.Estabelecimento", t => t.EstabelecimentoId)
                .ForeignKey("dbo.Produto", t => t.ProdutoId)
                .ForeignKey("dbo.Pessoa", t => t.VendedorId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.EstabelecimentoId)
                .Index(t => t.VendedorId)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataCriacao = c.DateTimeOffset(nullable: false, precision: 7),
                        DataModificacao = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indicacao", "PessoaId", "dbo.Pessoa");
            DropForeignKey("dbo.Movimento", "VendedorId", "dbo.Pessoa");
            DropForeignKey("dbo.Movimento", "ProdutoId", "dbo.Produto");
            DropForeignKey("dbo.Movimento", "EstabelecimentoId", "dbo.Estabelecimento");
            DropForeignKey("dbo.Movimento", "ClienteId", "dbo.Pessoa");
            DropForeignKey("dbo.Pessoa", "EstabelecimentoId", "dbo.Estabelecimento");
            DropForeignKey("dbo.Indicacao", "EstabelecimentoId", "dbo.Estabelecimento");
            DropForeignKey("dbo.Estabelecimento", "GrupoEstabelecimentoId", "dbo.GrupoEstabelecimento");
            DropIndex("dbo.Movimento", new[] { "ProdutoId" });
            DropIndex("dbo.Movimento", new[] { "VendedorId" });
            DropIndex("dbo.Movimento", new[] { "EstabelecimentoId" });
            DropIndex("dbo.Movimento", new[] { "ClienteId" });
            DropIndex("dbo.Pessoa", new[] { "EstabelecimentoId" });
            DropIndex("dbo.Pessoa", new[] { "Usuario" });
            DropIndex("dbo.Indicacao", new[] { "EstabelecimentoId" });
            DropIndex("dbo.Indicacao", new[] { "PessoaId" });
            DropIndex("dbo.Estabelecimento", new[] { "GrupoEstabelecimentoId" });
            DropTable("dbo.Produto");
            DropTable("dbo.Movimento");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Indicacao");
            DropTable("dbo.GrupoEstabelecimento");
            DropTable("dbo.Estabelecimento");
            DropTable("dbo.Configuracao");
        }
    }
}
