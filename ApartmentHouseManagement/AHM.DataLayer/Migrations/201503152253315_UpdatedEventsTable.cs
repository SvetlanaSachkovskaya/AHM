namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEventsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "ApartmentId", "dbo.Apartments");
            DropIndex("dbo.Events", new[] { "ApartmentId" });
            AddColumn("dbo.Events", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "CreateDate");
            DropColumn("dbo.Events", "ApartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ApartmentId", c => c.Int());
            AddColumn("dbo.Events", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "DateTime");
            CreateIndex("dbo.Events", "ApartmentId");
            AddForeignKey("dbo.Events", "ApartmentId", "dbo.Apartments", "Id");
        }
    }
}
