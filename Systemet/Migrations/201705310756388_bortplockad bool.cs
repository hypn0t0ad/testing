namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bortplockadbool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uppgifters", "bortplockad", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uppgifters", "bortplockad");
        }
    }
}
