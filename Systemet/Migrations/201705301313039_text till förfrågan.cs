namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class texttillförfrågan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GruppFörfrågan", "text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GruppFörfrågan", "text");
        }
    }
}
