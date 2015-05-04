namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedBillsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "IsClosed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bills", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bills", "IsClosed");
        }
    }
}
