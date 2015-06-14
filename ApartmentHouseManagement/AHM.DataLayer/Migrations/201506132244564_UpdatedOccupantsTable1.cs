namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOccupantsTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Occupants", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Occupants", "IsActive");
        }
    }
}
