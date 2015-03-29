namespace Bonificacao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndiceUnicoUsuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "Usuario", c => c.String(maxLength: 60));
            CreateIndex("dbo.Pessoa", "Usuario", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pessoa", new[] { "Usuario" });
            AlterColumn("dbo.Pessoa", "Usuario", c => c.String());
        }
    }
}
