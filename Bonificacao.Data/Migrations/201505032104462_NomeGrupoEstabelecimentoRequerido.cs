namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeGrupoEstabelecimentoRequerido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GrupoEstabelecimento", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GrupoEstabelecimento", "Nome", c => c.String());
        }
    }
}
