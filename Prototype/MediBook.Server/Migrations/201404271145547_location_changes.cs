namespace MediBook.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocationModels", "Latititude", c => c.Double(nullable: false));
            AddColumn("dbo.LocationModels", "Longititude", c => c.Double(nullable: false));
            DropColumn("dbo.LocationModels", "GoogleMapsUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LocationModels", "GoogleMapsUri", c => c.String());
            DropColumn("dbo.LocationModels", "Longititude");
            DropColumn("dbo.LocationModels", "Latititude");
        }
    }
}
