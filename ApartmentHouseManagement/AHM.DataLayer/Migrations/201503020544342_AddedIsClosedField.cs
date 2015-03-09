namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsClosedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructions", "IsClosed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Packages", "IsClosed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Packages", "IsOpen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "IsOpen", c => c.Boolean(nullable: false));
            DropColumn("dbo.Packages", "IsClosed");
            DropColumn("dbo.Instructions", "IsClosed");
        }
    }
}
