namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedInstructionAndLocationTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructions", "Title", c => c.String());
            AddColumn("dbo.Instructions", "ExecutionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Instructions", "CreateDate");
            DropColumn("dbo.Locations", "BackColorHexCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "BackColorHexCode", c => c.String());
            AddColumn("dbo.Instructions", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Instructions", "ExecutionDate");
            DropColumn("dbo.Instructions", "Title");
        }
    }
}
