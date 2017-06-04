namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedledareIDforgrupp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grupps", "LedareID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grupps", "LedareID");
        }
    }
}
