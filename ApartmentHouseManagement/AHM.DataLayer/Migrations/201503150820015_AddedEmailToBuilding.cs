namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailToBuilding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buildings", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buildings", "Email");
        }
    }
}
