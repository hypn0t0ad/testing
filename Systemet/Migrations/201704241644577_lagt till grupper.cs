namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lagttillgrupper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grupps",
                c => new
                    {
                        GruppID = c.Int(nullable: false, identity: true),
                        GruppNamn = c.String(),
                    })
                .PrimaryKey(t => t.GruppID);
            
            CreateTable(
                "dbo.GruppAnvändarKonton",
                c => new
                    {
                        Grupp_GruppID = c.Int(nullable: false),
                        AnvändarKonton_AnvändarID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Grupp_GruppID, t.AnvändarKonton_AnvändarID })
                .ForeignKey("dbo.Grupps", t => t.Grupp_GruppID, cascadeDelete: true)
                .ForeignKey("dbo.AnvändarKonton", t => t.AnvändarKonton_AnvändarID, cascadeDelete: true)
                .Index(t => t.Grupp_GruppID)
                .Index(t => t.AnvändarKonton_AnvändarID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GruppAnvändarKonton", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton");
            DropForeignKey("dbo.GruppAnvändarKonton", "Grupp_GruppID", "dbo.Grupps");
            DropIndex("dbo.GruppAnvändarKonton", new[] { "AnvändarKonton_AnvändarID" });
            DropIndex("dbo.GruppAnvändarKonton", new[] { "Grupp_GruppID" });
            DropTable("dbo.GruppAnvändarKonton");
            DropTable("dbo.Grupps");
        }
    }
}
