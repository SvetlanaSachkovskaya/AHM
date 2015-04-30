namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "EscrowBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bills", "CalculatedAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bills", "CarryOver", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bills", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bills", "TotalAmount");
            DropColumn("dbo.Bills", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bills", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Bills", "IsActive");
            DropColumn("dbo.Bills", "CarryOver");
            DropColumn("dbo.Bills", "CalculatedAmount");
            DropColumn("dbo.Apartments", "EscrowBalance");
        }
    }
}
