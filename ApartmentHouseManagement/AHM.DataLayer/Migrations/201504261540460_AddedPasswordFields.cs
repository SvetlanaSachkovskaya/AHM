using System.Data.Entity.Migrations;

namespace AHM.DataLayer.Migrations
{
    public partial class AddedPasswordFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Salt", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Salt");
            DropColumn("dbo.AspNetUsers", "Password");
        }
    }
}
