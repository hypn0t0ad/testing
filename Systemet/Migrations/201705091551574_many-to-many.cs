namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnvändarKontonGrupp", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton");
            DropForeignKey("dbo.AnvändarKontonGrupp", "Grupp_GruppID", "dbo.Grupps");
            DropIndex("dbo.AnvändarKontonGrupp", new[] { "AnvändarKonton_AnvändarID" });
            DropIndex("dbo.AnvändarKontonGrupp", new[] { "Grupp_GruppID" });
            AddColumn("dbo.Grupps", "AnvändarKonton_AnvändarID", c => c.Int());
            AddColumn("dbo.Grupps", "användare_AnvändarID", c => c.Int());
            AddColumn("dbo.AnvändarKonton", "grupper_GruppID", c => c.Int());
            AddColumn("dbo.AnvändarKonton", "Grupp_GruppID", c => c.Int());
            CreateIndex("dbo.Grupps", "AnvändarKonton_AnvändarID");
            CreateIndex("dbo.Grupps", "användare_AnvändarID");
            CreateIndex("dbo.AnvändarKonton", "grupper_GruppID");
            CreateIndex("dbo.AnvändarKonton", "Grupp_GruppID");
            AddForeignKey("dbo.AnvändarKonton", "grupper_GruppID", "dbo.Grupps", "GruppID");
            AddForeignKey("dbo.Grupps", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton", "AnvändarID");
            AddForeignKey("dbo.Grupps", "användare_AnvändarID", "dbo.AnvändarKonton", "AnvändarID");
            AddForeignKey("dbo.AnvändarKonton", "Grupp_GruppID", "dbo.Grupps", "GruppID");
            DropTable("dbo.AnvändarKontonGrupp");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AnvändarKontonGrupp",
                c => new
                    {
                        AnvändarKonton_AnvändarID = c.Int(nullable: false),
                        Grupp_GruppID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnvändarKonton_AnvändarID, t.Grupp_GruppID });
            
            DropForeignKey("dbo.AnvändarKonton", "Grupp_GruppID", "dbo.Grupps");
            DropForeignKey("dbo.Grupps", "användare_AnvändarID", "dbo.AnvändarKonton");
            DropForeignKey("dbo.Grupps", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton");
            DropForeignKey("dbo.AnvändarKonton", "grupper_GruppID", "dbo.Grupps");
            DropIndex("dbo.AnvändarKonton", new[] { "Grupp_GruppID" });
            DropIndex("dbo.AnvändarKonton", new[] { "grupper_GruppID" });
            DropIndex("dbo.Grupps", new[] { "användare_AnvändarID" });
            DropIndex("dbo.Grupps", new[] { "AnvändarKonton_AnvändarID" });
            DropColumn("dbo.AnvändarKonton", "Grupp_GruppID");
            DropColumn("dbo.AnvändarKonton", "grupper_GruppID");
            DropColumn("dbo.Grupps", "användare_AnvändarID");
            DropColumn("dbo.Grupps", "AnvändarKonton_AnvändarID");
            CreateIndex("dbo.AnvändarKontonGrupp", "Grupp_GruppID");
            CreateIndex("dbo.AnvändarKontonGrupp", "AnvändarKonton_AnvändarID");
            AddForeignKey("dbo.AnvändarKontonGrupp", "Grupp_GruppID", "dbo.Grupps", "GruppID", cascadeDelete: true);
            AddForeignKey("dbo.AnvändarKontonGrupp", "AnvändarKonton_AnvändarID", "dbo.AnvändarKonton", "AnvändarID", cascadeDelete: true);
        }
    }
}
