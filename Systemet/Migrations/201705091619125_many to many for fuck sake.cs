namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomanyforfucksake : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnvändarKonton", "grupper_GruppID", "dbo.Grupps");
            DropForeignKey("dbo.Grupps", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton");
            DropForeignKey("dbo.Grupps", "användare_AnvändarID", "dbo.AnvändarKonton");
            DropForeignKey("dbo.AnvändarKonton", "Grupp_GruppID", "dbo.Grupps");
            DropIndex("dbo.Grupps", new[] { "AnvändarKonton_AnvändarID" });
            DropIndex("dbo.Grupps", new[] { "användare_AnvändarID" });
            DropIndex("dbo.AnvändarKonton", new[] { "grupper_GruppID" });
            DropIndex("dbo.AnvändarKonton", new[] { "Grupp_GruppID" });
            CreateTable(
                "dbo.AnvändarKontonGrupp",
                c => new
                    {
                        AnvändarKonton_AnvändarID = c.Int(nullable: false),
                        Grupp_GruppID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnvändarKonton_AnvändarID, t.Grupp_GruppID })
                .ForeignKey("dbo.AnvändarKonton", t => t.AnvändarKonton_AnvändarID, cascadeDelete: true)
                .ForeignKey("dbo.Grupps", t => t.Grupp_GruppID, cascadeDelete: true)
                .Index(t => t.AnvändarKonton_AnvändarID)
                .Index(t => t.Grupp_GruppID);
            
            DropColumn("dbo.Grupps", "AnvändarKonton_AnvändarID");
            DropColumn("dbo.Grupps", "användare_AnvändarID");
            DropColumn("dbo.AnvändarKonton", "grupper_GruppID");
            DropColumn("dbo.AnvändarKonton", "Grupp_GruppID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnvändarKonton", "Grupp_GruppID", c => c.Int());
            AddColumn("dbo.AnvändarKonton", "grupper_GruppID", c => c.Int());
            AddColumn("dbo.Grupps", "användare_AnvändarID", c => c.Int());
            AddColumn("dbo.Grupps", "AnvändarKonton_AnvändarID", c => c.Int());
            DropForeignKey("dbo.AnvändarKontonGrupp", "Grupp_GruppID", "dbo.Grupps");
            DropForeignKey("dbo.AnvändarKontonGrupp", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton");
            DropIndex("dbo.AnvändarKontonGrupp", new[] { "Grupp_GruppID" });
            DropIndex("dbo.AnvändarKontonGrupp", new[] { "AnvändarKonton_AnvändarID" });
            DropTable("dbo.AnvändarKontonGrupp");
            CreateIndex("dbo.AnvändarKonton", "Grupp_GruppID");
            CreateIndex("dbo.AnvändarKonton", "grupper_GruppID");
            CreateIndex("dbo.Grupps", "användare_AnvändarID");
            CreateIndex("dbo.Grupps", "AnvändarKonton_AnvändarID");
            AddForeignKey("dbo.AnvändarKonton", "Grupp_GruppID", "dbo.Grupps", "GruppID");
            AddForeignKey("dbo.Grupps", "användare_AnvändarID", "dbo.AnvändarKonton", "AnvändarID");
            AddForeignKey("dbo.Grupps", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton", "AnvändarID");
            AddForeignKey("dbo.AnvändarKonton", "grupper_GruppID", "dbo.Grupps", "GruppID");
        }
    }
}
