namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valideringar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AnvändarKonton", "FörNamn", c => c.String(nullable: false));
            AlterColumn("dbo.AnvändarKonton", "EfterNamn", c => c.String(nullable: false));
            AlterColumn("dbo.AnvändarKonton", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.AnvändarKonton", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AnvändarKonton", "Password", c => c.String());
            AlterColumn("dbo.AnvändarKonton", "Email", c => c.String());
            AlterColumn("dbo.AnvändarKonton", "EfterNamn", c => c.String());
            AlterColumn("dbo.AnvändarKonton", "FörNamn", c => c.String());
        }
    }
}
