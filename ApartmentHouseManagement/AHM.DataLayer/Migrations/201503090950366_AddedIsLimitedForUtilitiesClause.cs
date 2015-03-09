namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsLimitedForUtilitiesClause : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UtilitiesClauses", "IsLimited", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UtilitiesClauses", "IsLimited");
        }
    }
}
