namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeProdutoRequerido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "Nome", c => c.String());
        }
    }
}
