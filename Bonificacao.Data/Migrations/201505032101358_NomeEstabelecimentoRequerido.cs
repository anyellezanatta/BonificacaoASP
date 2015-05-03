namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeEstabelecimentoRequerido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estabelecimento", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estabelecimento", "Nome", c => c.String());
        }
    }
}
