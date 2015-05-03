namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovidoDataHoraMovimento : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movimento", "DataHoraMovimento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movimento", "DataHoraMovimento", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
