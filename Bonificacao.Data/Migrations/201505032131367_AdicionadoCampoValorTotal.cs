namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoCampoValorTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimento", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movimento", "ValorTotal");
        }
    }
}
