namespace BitBookWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBasicInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasicInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProfilePicUrl = c.String(),
                        CoverPicUrl = c.String(),
                        About = c.String(),
                        AreaOfInterest = c.String(),
                        Location = c.String(),
                        Education = c.String(),
                        Experience = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BasicInfoes", new[] { "UserId" });
            DropTable("dbo.BasicInfoes");
        }
    }
}
