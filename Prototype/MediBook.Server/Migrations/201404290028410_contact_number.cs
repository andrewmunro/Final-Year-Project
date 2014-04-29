namespace MediBook.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contact_number : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocationModels", "ContactNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocationModels", "ContactNumber");
        }
    }
}
