namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullablePaidDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bills", "PaidDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bills", "PaidDate", c => c.DateTime(nullable: false));
        }
    }
}
