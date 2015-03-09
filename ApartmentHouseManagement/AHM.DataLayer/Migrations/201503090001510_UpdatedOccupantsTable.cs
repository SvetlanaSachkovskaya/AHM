namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOccupantsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "Name", c => c.String());
            AddColumn("dbo.Apartments", "Square", c => c.Double(nullable: false));
            AddColumn("dbo.Occupants", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Occupants", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Occupants", "IsOwner", c => c.Boolean(nullable: false));
            DropColumn("dbo.Occupants", "FirstName");
            DropColumn("dbo.Occupants", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Occupants", "LastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Occupants", "FirstName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Occupants", "IsOwner");
            DropColumn("dbo.Occupants", "DateOfBirth");
            DropColumn("dbo.Occupants", "Name");
            DropColumn("dbo.Apartments", "Square");
            DropColumn("dbo.Apartments", "Name");
        }
    }
}
