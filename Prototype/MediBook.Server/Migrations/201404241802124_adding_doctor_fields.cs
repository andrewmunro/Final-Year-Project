namespace MediBook.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_doctor_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorModels", "ImageURL", c => c.String());
            AddColumn("dbo.DoctorModels", "DoctorType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoctorModels", "DoctorType");
            DropColumn("dbo.DoctorModels", "ImageURL");
        }
    }
}
