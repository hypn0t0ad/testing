namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class användarkommentarer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvenemangsKommentarers", "kommentator_AnvändarID", c => c.Int());
            CreateIndex("dbo.EvenemangsKommentarers", "kommentator_AnvändarID");
            AddForeignKey("dbo.EvenemangsKommentarers", "kommentator_AnvändarID", "dbo.AnvändarKonton", "AnvändarID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvenemangsKommentarers", "kommentator_AnvändarID", "dbo.AnvändarKonton");
            DropIndex("dbo.EvenemangsKommentarers", new[] { "kommentator_AnvändarID" });
            DropColumn("dbo.EvenemangsKommentarers", "kommentator_AnvändarID");
        }
    }
}
