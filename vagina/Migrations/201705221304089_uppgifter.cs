namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uppgifter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Uppgifters", "slutdatum", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uppgifters", "slutdatum", c => c.DateTime(nullable: false));
        }
    }
}
