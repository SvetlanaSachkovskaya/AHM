namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEventsAndInstructions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "Occupant_Id", "dbo.Occupants");
            DropIndex("dbo.Packages", new[] { "Occupant_Id" });
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        Content = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ApartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildingId = c.Int(nullable: false),
                        Content = c.String(),
                        EmploeeId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EmploeeId, cascadeDelete: true)
                .Index(t => t.BuildingId)
                .Index(t => t.EmploeeId);
            
            DropColumn("dbo.Packages", "Occupant_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Occupant_Id", c => c.Int());
            DropForeignKey("dbo.Instructions", "EmploeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Instructions", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Events", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Events", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.Instructions", new[] { "EmploeeId" });
            DropIndex("dbo.Instructions", new[] { "BuildingId" });
            DropIndex("dbo.Events", new[] { "ApartmentId" });
            DropIndex("dbo.Events", new[] { "BuildingId" });
            DropTable("dbo.Instructions");
            DropTable("dbo.Events");
            CreateIndex("dbo.Packages", "Occupant_Id");
            AddForeignKey("dbo.Packages", "Occupant_Id", "dbo.Occupants", "Id");
        }
    }
}
