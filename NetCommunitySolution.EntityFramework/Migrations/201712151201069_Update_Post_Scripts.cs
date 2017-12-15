namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Post_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Answer", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Support", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Support");
            DropColumn("dbo.Posts", "Views");
            DropColumn("dbo.Posts", "Answer");
        }
    }
}
