namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_CustomerAttribute_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 200),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerAttributes");
        }
    }
}
