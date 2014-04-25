namespace MediBook.Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentModels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        RequiredAppointmentSlots = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ScheduledTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Priority = c.Int(nullable: false),
                        Doctor_UserName = c.String(maxLength: 128),
                        Location_Name = c.String(maxLength: 128),
                        Patient_UserName = c.String(maxLength: 128),
                        Type_Type = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DoctorModels", t => t.Doctor_UserName)
                .ForeignKey("dbo.LocationModels", t => t.Location_Name)
                .ForeignKey("dbo.PatientModels", t => t.Patient_UserName)
                .ForeignKey("dbo.AppointmentTypeModels", t => t.Type_Type)
                .Index(t => t.Doctor_UserName)
                .Index(t => t.Location_Name)
                .Index(t => t.Patient_UserName)
                .Index(t => t.Type_Type);
            
            CreateTable(
                "dbo.DoctorModels",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AppointmentTypeModel_Type = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.AppointmentTypeModels", t => t.AppointmentTypeModel_Type)
                .Index(t => t.AppointmentTypeModel_Type);
            
            CreateTable(
                "dbo.LocationModels",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        GoogleMapsUri = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.PatientModels",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.AppointmentTypeModels",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 128),
                        TimeSlot = c.Int(nullable: false),
                        Description = c.String(),
                        CreatableByPatients = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Type);
            
            CreateTable(
                "dbo.NotificationModels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DueTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Title = c.String(),
                        Body = c.String(),
                        Appointment_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppointmentModels", t => t.Appointment_ID)
                .Index(t => t.Appointment_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationModels", "Appointment_ID", "dbo.AppointmentModels");
            DropForeignKey("dbo.AppointmentModels", "Type_Type", "dbo.AppointmentTypeModels");
            DropForeignKey("dbo.DoctorModels", "AppointmentTypeModel_Type", "dbo.AppointmentTypeModels");
            DropForeignKey("dbo.AppointmentModels", "Patient_UserName", "dbo.PatientModels");
            DropForeignKey("dbo.AppointmentModels", "Location_Name", "dbo.LocationModels");
            DropForeignKey("dbo.AppointmentModels", "Doctor_UserName", "dbo.DoctorModels");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.NotificationModels", new[] { "Appointment_ID" });
            DropIndex("dbo.AppointmentModels", new[] { "Type_Type" });
            DropIndex("dbo.DoctorModels", new[] { "AppointmentTypeModel_Type" });
            DropIndex("dbo.AppointmentModels", new[] { "Patient_UserName" });
            DropIndex("dbo.AppointmentModels", new[] { "Location_Name" });
            DropIndex("dbo.AppointmentModels", new[] { "Doctor_UserName" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NotificationModels");
            DropTable("dbo.AppointmentTypeModels");
            DropTable("dbo.PatientModels");
            DropTable("dbo.LocationModels");
            DropTable("dbo.DoctorModels");
            DropTable("dbo.AppointmentModels");
        }
    }
}
