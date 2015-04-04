namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacaoMovimento : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Movimento", name: "FrentistaId", newName: "VendedorId");
            RenameIndex(table: "dbo.Movimento", name: "IX_FrentistaId", newName: "IX_VendedorId");
            AddColumn("dbo.Movimento", "ValorBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Movimento", "SaldoBonus", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Movimento", "Debito");
            DropColumn("dbo.Movimento", "Credito");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movimento", "Credito", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Movimento", "Debito", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Movimento", "SaldoBonus");
            DropColumn("dbo.Movimento", "ValorBonus");
            RenameIndex(table: "dbo.Movimento", name: "IX_VendedorId", newName: "IX_FrentistaId");
            RenameColumn(table: "dbo.Movimento", name: "VendedorId", newName: "FrentistaId");
        }
    }
}
