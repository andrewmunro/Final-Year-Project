namespace MediBook.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatinggcmfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorModels", "GcmRegistrationId", c => c.String());
            AddColumn("dbo.PatientModels", "GcmRegistrationId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientModels", "GcmRegistrationId");
            DropColumn("dbo.DoctorModels", "GcmRegistrationId");
        }
    }
}
