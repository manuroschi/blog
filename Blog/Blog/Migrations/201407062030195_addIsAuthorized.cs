namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsAuthorized : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsAuthorized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsAuthorized");
        }
    }
}
