namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lagttillevenemangskommentarer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvenemangsKommentarers",
                c => new
                    {
                        EvenemangsKommentarerID = c.Int(nullable: false, identity: true),
                        TidenFörKommentaren = c.DateTime(nullable: false),
                        Text = c.String(),
                        evenemang_EvenemangID = c.Int(),
                    })
                .PrimaryKey(t => t.EvenemangsKommentarerID)
                .ForeignKey("dbo.Evenemangs", t => t.evenemang_EvenemangID)
                .Index(t => t.evenemang_EvenemangID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvenemangsKommentarers", "evenemang_EvenemangID", "dbo.Evenemangs");
            DropIndex("dbo.EvenemangsKommentarers", new[] { "evenemang_EvenemangID" });
            DropTable("dbo.EvenemangsKommentarers");
        }
    }
}
