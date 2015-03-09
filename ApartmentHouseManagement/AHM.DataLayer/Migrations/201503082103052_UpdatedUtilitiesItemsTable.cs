namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUtilitiesItemsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UtilitiesItems", "SubsidezedAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.UtilitiesItems", "AmountByFullTariff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.UtilitiesItems", "Amount");
            DropColumn("dbo.UtilitiesItems", "Overpay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UtilitiesItems", "Overpay", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.UtilitiesItems", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.UtilitiesItems", "AmountByFullTariff");
            DropColumn("dbo.UtilitiesItems", "SubsidezedAmount");
        }
    }
}
