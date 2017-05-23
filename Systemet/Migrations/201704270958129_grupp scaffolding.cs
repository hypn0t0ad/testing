namespace Systemet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gruppscaffolding : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GruppAnvändarKonton", newName: "AnvändarKontonGrupp");
            DropPrimaryKey("dbo.AnvändarKontonGrupp");
            AddPrimaryKey("dbo.AnvändarKontonGrupp", new[] { "AnvändarKonton_AnvändarID", "Grupp_GruppID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AnvändarKontonGrupp");
            AddPrimaryKey("dbo.AnvändarKontonGrupp", new[] { "Grupp_GruppID", "AnvändarKonton_AnvändarID" });
            RenameTable(name: "dbo.AnvändarKontonGrupp", newName: "GruppAnvändarKonton");
        }
    }
}
