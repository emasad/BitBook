namespace BitBookWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFrindClassUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserFriends", "CurrentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFriends", "CurrentDate", c => c.DateTime(nullable: false));
        }
    }
}
