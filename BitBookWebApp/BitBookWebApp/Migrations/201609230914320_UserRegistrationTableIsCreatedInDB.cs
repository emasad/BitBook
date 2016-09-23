namespace BitBookWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRegistrationTableIsCreatedInDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 10, unicode: false),
                        SurName = c.String(maxLength: 10, unicode: false),
                        Email = c.String(nullable: false, maxLength: 450),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.Users");
        }
    }
}
