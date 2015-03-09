namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUtilities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApartmentId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaid = c.Boolean(nullable: false),
                        IsEmailSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Apartments", t => t.ApartmentId, cascadeDelete: true)
                .Index(t => t.ApartmentId);
            
            CreateTable(
                "dbo.UtilitiesClauses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UtiliitesClauseType = c.Int(nullable: false),
                        Measure = c.String(),
                        CalculationType = c.Int(nullable: false),
                        SubsidizedTariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FullTariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        LimitForPerson = c.Double(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.UtilitiesItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UtilitiesClauseId = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bills", t => t.BillId)
                .ForeignKey("dbo.UtilitiesClauses", t => t.UtilitiesClauseId, cascadeDelete: true)
                .Index(t => t.UtilitiesClauseId)
                .Index(t => t.BillId);
            
            AddColumn("dbo.Events", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UtilitiesItems", "UtilitiesClauseId", "dbo.UtilitiesClauses");
            DropForeignKey("dbo.UtilitiesItems", "BillId", "dbo.Bills");
            DropForeignKey("dbo.UtilitiesClauses", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Bills", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.UtilitiesItems", new[] { "BillId" });
            DropIndex("dbo.UtilitiesItems", new[] { "UtilitiesClauseId" });
            DropIndex("dbo.UtilitiesClauses", new[] { "BuildingId" });
            DropIndex("dbo.Bills", new[] { "ApartmentId" });
            DropColumn("dbo.Events", "IsRemoved");
            DropTable("dbo.UtilitiesItems");
            DropTable("dbo.UtilitiesClauses");
            DropTable("dbo.Bills");
        }
    }
}
