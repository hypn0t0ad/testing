namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gruppbeskrivning : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grupps", "beskrivning", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grupps", "beskrivning");
        }
    }
}
