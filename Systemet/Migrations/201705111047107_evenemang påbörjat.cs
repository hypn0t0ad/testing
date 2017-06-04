namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class evenemangpåbörjat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evenemangs",
                c => new
                    {
                        EvenemangID = c.Int(nullable: false, identity: true),
                        Namn = c.String(),
                        Beskrivning = c.String(),
                        StartTid = c.DateTime(nullable: false),
                        SlutTid = c.DateTime(nullable: false),
                        Dag = c.DateTime(nullable: false),
                        grupp_GruppID = c.Int(),
                    })
                .PrimaryKey(t => t.EvenemangID)
                .ForeignKey("dbo.Grupps", t => t.grupp_GruppID)
                .Index(t => t.grupp_GruppID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evenemangs", "grupp_GruppID", "dbo.Grupps");
            DropIndex("dbo.Evenemangs", new[] { "grupp_GruppID" });
            DropTable("dbo.Evenemangs");
        }
    }
}
