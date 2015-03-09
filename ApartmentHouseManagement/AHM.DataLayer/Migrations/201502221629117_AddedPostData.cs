namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        LongDescription = c.String(),
                        ShortDescription = c.String(),
                        BackColorHexCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OpenDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(),
                        OpenComment = c.String(),
                        CloseComment = c.String(),
                        IsOpen = c.Boolean(nullable: false),
                        LastChangeDate = c.DateTime(nullable: false),
                        OpenPhoto = c.Binary(),
                        ClosePhoto = c.Binary(),
                        OpenedByEmployeeId = c.Int(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                        NotificationOptionsId = c.Int(nullable: false),
                        LocationId = c.Int(),
                        PackageTypeId = c.Int(nullable: false),
                        Occupant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Occupants", t => t.Occupant_Id)
                .ForeignKey("dbo.NotificationOptions", t => t.NotificationOptionsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.OpenedByEmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.PackageTypes", t => t.PackageTypeId, cascadeDelete: true)
                .Index(t => t.OpenedByEmployeeId)
                .Index(t => t.ApartmentId)
                .Index(t => t.NotificationOptionsId)
                .Index(t => t.LocationId)
                .Index(t => t.PackageTypeId)
                .Index(t => t.Occupant_Id);
            
            CreateTable(
                "dbo.NotificationOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOpenNotificationSent = c.Boolean(nullable: false),
                        ShouldNotifyAllOccupants = c.Boolean(nullable: false),
                        OccupantId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Occupants", t => t.OccupantId)
                .Index(t => t.OccupantId);
            
            CreateTable(
                "dbo.Occupants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(),
                        IsSubTenant = c.Boolean(nullable: false),
                        ApartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.PackageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        ShortDescription = c.String(),
                        LongDescription = c.String(),
                        PhotoSettings_IsRequiredOnOpen = c.Boolean(nullable: false),
                        PhotoSettings_IsEnabledOnOpen = c.Boolean(nullable: false),
                        PhotoSettings_IsRequiredOnClose = c.Boolean(nullable: false),
                        PhotoSettings_IsEnabledOnClose = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: false)
                .Index(t => t.BuildingId);
            
            AddColumn("dbo.AspNetUsers", "BuildingId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "BuildingId");
            AddForeignKey("dbo.AspNetUsers", "BuildingId", "dbo.Buildings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "PackageTypeId", "dbo.PackageTypes");
            DropForeignKey("dbo.PackageTypes", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Packages", "OpenedByEmployeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Packages", "NotificationOptionsId", "dbo.NotificationOptions");
            DropForeignKey("dbo.NotificationOptions", "OccupantId", "dbo.Occupants");
            DropForeignKey("dbo.Packages", "Occupant_Id", "dbo.Occupants");
            DropForeignKey("dbo.Occupants", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Packages", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Packages", "ApartmentId", "dbo.Apartments");
            DropForeignKey("dbo.Locations", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Apartments", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.PackageTypes", new[] { "BuildingId" });
            DropIndex("dbo.AspNetUsers", new[] { "BuildingId" });
            DropIndex("dbo.Occupants", new[] { "ApartmentId" });
            DropIndex("dbo.NotificationOptions", new[] { "OccupantId" });
            DropIndex("dbo.Packages", new[] { "Occupant_Id" });
            DropIndex("dbo.Packages", new[] { "PackageTypeId" });
            DropIndex("dbo.Packages", new[] { "LocationId" });
            DropIndex("dbo.Packages", new[] { "NotificationOptionsId" });
            DropIndex("dbo.Packages", new[] { "ApartmentId" });
            DropIndex("dbo.Packages", new[] { "OpenedByEmployeeId" });
            DropIndex("dbo.Locations", new[] { "BuildingId" });
            DropIndex("dbo.Apartments", new[] { "BuildingId" });
            DropColumn("dbo.AspNetUsers", "BuildingId");
            DropTable("dbo.PackageTypes");
            DropTable("dbo.Occupants");
            DropTable("dbo.NotificationOptions");
            DropTable("dbo.Packages");
            DropTable("dbo.Locations");
            DropTable("dbo.Buildings");
            DropTable("dbo.Apartments");
        }
    }
}
