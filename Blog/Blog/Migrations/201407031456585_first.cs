namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(),
                        Author_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.Author_UserId)
                .Index(t => t.Author_UserId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(),
                        Replier_UserId = c.Int(),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.Replier_UserId)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Replier_UserId)
                .Index(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Replies", new[] { "Post_Id" });
            DropIndex("dbo.Replies", new[] { "Replier_UserId" });
            DropIndex("dbo.Tags", new[] { "Post_Id" });
            DropIndex("dbo.Posts", new[] { "Author_UserId" });
            DropForeignKey("dbo.Replies", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Replies", "Replier_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Tags", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Author_UserId", "dbo.UserProfile");
            DropTable("dbo.Replies");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.UserProfile");
        }
    }
}
