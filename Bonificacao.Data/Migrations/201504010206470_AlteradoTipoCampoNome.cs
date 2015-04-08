namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteradoTipoCampoNome : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GrupoEstabelecimento", "Nome", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GrupoEstabelecimento", "Nome", c => c.Int(nullable: false));
        }
    }
}
