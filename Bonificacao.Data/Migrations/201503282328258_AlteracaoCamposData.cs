namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoCamposData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Configuracao", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Configuracao", "DataModificacao", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Estabelecimento", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Estabelecimento", "DataModificacao", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.GrupoEstabelecimento", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.GrupoEstabelecimento", "DataModificacao", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Movimento", "DataHoraMovimento", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Movimento", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Movimento", "DataModificacao", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Pessoa", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Pessoa", "DataModificacao", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Indicacao", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Indicacao", "DataModificacao", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Produto", "DataCriacao", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Produto", "DataModificacao", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.Produto", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Indicacao", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.Indicacao", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pessoa", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.Pessoa", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movimento", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.Movimento", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Movimento", "DataHoraMovimento", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GrupoEstabelecimento", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.GrupoEstabelecimento", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Estabelecimento", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.Estabelecimento", "DataCriacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Configuracao", "DataModificacao", c => c.DateTime());
            AlterColumn("dbo.Configuracao", "DataCriacao", c => c.DateTime(nullable: false));
        }
    }
}
