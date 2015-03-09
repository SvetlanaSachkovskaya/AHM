namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedUtilitiesClauseTypeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UtilitiesClauses", "UtilitiesClauseType", c => c.Int(nullable: false));
            DropColumn("dbo.UtilitiesClauses", "UtiliitesClauseType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UtilitiesClauses", "UtiliitesClauseType", c => c.Int(nullable: false));
            DropColumn("dbo.UtilitiesClauses", "UtilitiesClauseType");
        }
    }
}
