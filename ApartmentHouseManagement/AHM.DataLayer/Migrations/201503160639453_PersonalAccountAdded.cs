namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonalAccountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apartments", "PersonalAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Apartments", "PersonalAccount");
        }
    }
}
