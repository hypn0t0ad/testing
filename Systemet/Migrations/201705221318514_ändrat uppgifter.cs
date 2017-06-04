namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ändratuppgifter : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Uppgifters", "slutdatum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uppgifters", "slutdatum", c => c.String());
        }
    }
}
