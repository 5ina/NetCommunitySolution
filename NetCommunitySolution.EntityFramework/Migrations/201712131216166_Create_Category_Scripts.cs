namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Category_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Description = c.String(),
                        CategoryTemplateNames = c.String(maxLength: 15),
                        MetaKeywords = c.String(maxLength: 200),
                        MetaDescription = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        ParentCategoryId = c.Int(nullable: false),
                        IncludeInTopMenu = c.Boolean(nullable: false),
                        Published = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
