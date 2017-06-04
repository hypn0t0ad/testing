namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gruppförfrågningar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GruppFörfrågan",
                c => new
                    {
                        FörfråganID = c.Int(nullable: false, identity: true),
                        Godkänd = c.Boolean(nullable: false),
                        AnvändareSomFrågar_AnvändarID = c.Int(),
                        GruppFörfråganGäller_GruppID = c.Int(),
                    })
                .PrimaryKey(t => t.FörfråganID)
                .ForeignKey("dbo.AnvändarKonton", t => t.AnvändareSomFrågar_AnvändarID)
                .ForeignKey("dbo.Grupps", t => t.GruppFörfråganGäller_GruppID)
                .Index(t => t.AnvändareSomFrågar_AnvändarID)
                .Index(t => t.GruppFörfråganGäller_GruppID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GruppFörfrågan", "GruppFörfråganGäller_GruppID", "dbo.Grupps");
            DropForeignKey("dbo.GruppFörfrågan", "AnvändareSomFrågar_AnvändarID", "dbo.AnvändarKonton");
            DropIndex("dbo.GruppFörfrågan", new[] { "GruppFörfråganGäller_GruppID" });
            DropIndex("dbo.GruppFörfrågan", new[] { "AnvändareSomFrågar_AnvändarID" });
            DropTable("dbo.GruppFörfrågan");
        }
    }
}
