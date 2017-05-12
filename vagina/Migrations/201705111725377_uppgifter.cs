namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uppgifter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uppgifter",
                c => new
                {
                    UppgifterID = c.Int(nullable: false, identity: true),
                    Namn = c.String(),
                    Beskrivning = c.String(),
                    Utf�rd = c.Boolean(),
                    P�b�rjad = c.Boolean(),
                    Startdatum = c.DateTime(),
                    Slutdatum = c.DateTime(),

                    Anv�ndarKonton_Anv�ndarID = c.Int(),
                    Grupp_GruppID = c.Int(),
                })
                .PrimaryKey(t => t.UppgifterID)
                .ForeignKey("dbo.Grupps", t => t.Grupp_GruppID)
                .ForeignKey("dbo.Anv�ndarKonton", t => t.Anv�ndarKonton_Anv�ndarID)
                .Index(t => t.Grupp_GruppID)
                .Index(t => t.Anv�ndarKonton_Anv�ndarID);
        }

        public override void Down()
        {
            DropTable("dbo.Uppgifter");
        }
    }
}
