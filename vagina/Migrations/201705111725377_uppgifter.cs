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
                    Utförd = c.Boolean(),
                    Påbörjad = c.Boolean(),
                    Startdatum = c.DateTime(),
                    Slutdatum = c.DateTime(),

                    AnvändarKonton_AnvändarID = c.Int(),
                    Grupp_GruppID = c.Int(),
                })
                .PrimaryKey(t => t.UppgifterID)
                .ForeignKey("dbo.Grupps", t => t.Grupp_GruppID)
                .ForeignKey("dbo.AnvändarKonton", t => t.AnvändarKonton_AnvändarID)
                .Index(t => t.Grupp_GruppID)
                .Index(t => t.AnvändarKonton_AnvändarID);
        }

        public override void Down()
        {
            DropTable("dbo.Uppgifter");
        }
    }
}
