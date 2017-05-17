namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagitbortstarttidochändratdagtilltidpunkt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evenemangs", "Tidpunkt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Evenemangs", "StartTid");
            DropColumn("dbo.Evenemangs", "Dag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Evenemangs", "Dag", c => c.DateTime(nullable: false));
            AddColumn("dbo.Evenemangs", "StartTid", c => c.DateTime(nullable: false));
            DropColumn("dbo.Evenemangs", "Tidpunkt");
        }
    }
}
