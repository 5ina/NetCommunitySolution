namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_RewardPointHistory_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RewardPointsHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        RewardPointModeId = c.Short(nullable: false),
                        Points = c.Int(nullable: false),
                        PointsBalance = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 200),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RewardPointsHistories");
        }
    }
}
