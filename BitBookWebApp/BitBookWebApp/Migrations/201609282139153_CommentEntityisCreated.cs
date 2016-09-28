namespace BitBookWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentEntityisCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                        PostText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserComments");
        }
    }
}
