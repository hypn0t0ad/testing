namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnvändarKonton",
                c => new
                    {
                        AnvändarID = c.Int(nullable: false, identity: true),
                        FörNamn = c.String(nullable: false),
                        EfterNamn = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefon = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.AnvändarID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnvändarKonton");
        }
    }
}
