namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uppgifter1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uppgifters",
                c => new
                    {
                        UppgifterID = c.Int(nullable: false, identity: true),
                        Namn = c.String(),
                        Beskrivning = c.String(),
                        Utförd = c.Boolean(nullable: false),
                        Påbörjad = c.Boolean(nullable: false),
                        Startdatum = c.DateTime(nullable: false),
                        Slutdatum = c.DateTime(nullable: false),
                        Ansvarig_AnvändarID = c.Int(),
                        TillhörGrupp_GruppID = c.Int(),
                    })
                .PrimaryKey(t => t.UppgifterID)
                .ForeignKey("dbo.AnvändarKonton", t => t.Ansvarig_AnvändarID)
                .ForeignKey("dbo.Grupps", t => t.TillhörGrupp_GruppID)
                .Index(t => t.Ansvarig_AnvändarID)
                .Index(t => t.TillhörGrupp_GruppID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uppgifters", "TillhörGrupp_GruppID", "dbo.Grupps");
            DropForeignKey("dbo.Uppgifters", "Ansvarig_AnvändarID", "dbo.AnvändarKonton");
            DropIndex("dbo.Uppgifters", new[] { "TillhörGrupp_GruppID" });
            DropIndex("dbo.Uppgifters", new[] { "Ansvarig_AnvändarID" });
            DropTable("dbo.Uppgifters");
        }
    }
}
