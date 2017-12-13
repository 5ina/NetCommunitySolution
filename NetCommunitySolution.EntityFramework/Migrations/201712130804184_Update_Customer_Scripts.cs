namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Customer_Scripts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "LoginName", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "LoginName");
        }
    }
}
