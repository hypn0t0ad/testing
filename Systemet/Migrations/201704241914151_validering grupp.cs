namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valideringgrupp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grupps", "GruppNamn", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grupps", "GruppNamn", c => c.String());
        }
    }
}
