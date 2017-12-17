namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Post_And_Comment_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Solve", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostComments", "Selected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostComments", "Selected");
            DropColumn("dbo.Posts", "Solve");
        }
    }
}
