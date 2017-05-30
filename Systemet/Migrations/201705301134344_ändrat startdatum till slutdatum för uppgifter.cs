namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ändratstartdatumtillslutdatumföruppgifter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uppgifters", "Slutdatum", c => c.DateTime(nullable: false));
            DropColumn("dbo.Uppgifters", "Startdatum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uppgifters", "Startdatum", c => c.DateTime(nullable: false));
            DropColumn("dbo.Uppgifters", "Slutdatum");
        }
    }
}
