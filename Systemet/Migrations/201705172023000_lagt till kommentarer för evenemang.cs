namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lagttillkommentarerförevenemang : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Evenemangs", "SlutTid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Evenemangs", "SlutTid", c => c.DateTime(nullable: false));
        }
    }
}
