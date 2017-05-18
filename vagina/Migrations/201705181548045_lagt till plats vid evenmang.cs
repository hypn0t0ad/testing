namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lagttillplatsvidevenmang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evenemangs", "Plats", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evenemangs", "Plats");
        }
    }
}
