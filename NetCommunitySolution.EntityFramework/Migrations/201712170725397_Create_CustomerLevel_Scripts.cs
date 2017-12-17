namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_CustomerLevel_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 10),
                        Level = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.ContentLabels", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContentLabels", "Name", c => c.String());
            DropTable("dbo.CustomerLevels");
        }
    }
}
