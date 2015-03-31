namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buildings", "FinePercent", c => c.Double(nullable: false));
            AddColumn("dbo.Buildings", "LastPayUtilitiesDay", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "PaidAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bills", "PaidDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bills", "Fine", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Buildings", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Buildings", "Email", c => c.String());
            DropColumn("dbo.Bills", "Fine");
            DropColumn("dbo.Bills", "PaidDate");
            DropColumn("dbo.Bills", "PaidAmount");
            DropColumn("dbo.Buildings", "LastPayUtilitiesDay");
            DropColumn("dbo.Buildings", "FinePercent");
        }
    }
}
