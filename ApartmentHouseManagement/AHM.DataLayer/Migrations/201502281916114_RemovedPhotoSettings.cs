namespace AHM.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPhotoSettings : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PackageTypes", "PhotoSettings_IsRequiredOnOpen");
            DropColumn("dbo.PackageTypes", "PhotoSettings_IsEnabledOnOpen");
            DropColumn("dbo.PackageTypes", "PhotoSettings_IsRequiredOnClose");
            DropColumn("dbo.PackageTypes", "PhotoSettings_IsEnabledOnClose");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PackageTypes", "PhotoSettings_IsEnabledOnClose", c => c.Boolean(nullable: false));
            AddColumn("dbo.PackageTypes", "PhotoSettings_IsRequiredOnClose", c => c.Boolean(nullable: false));
            AddColumn("dbo.PackageTypes", "PhotoSettings_IsEnabledOnOpen", c => c.Boolean(nullable: false));
            AddColumn("dbo.PackageTypes", "PhotoSettings_IsRequiredOnOpen", c => c.Boolean(nullable: false));
        }
    }
}
