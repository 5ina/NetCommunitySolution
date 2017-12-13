namespace NetCommunitySolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Customer_Scripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mobile = c.String(maxLength: 15),
                        Email = c.String(maxLength: 50),
                        NickName = c.String(maxLength: 30),
                        Level = c.Int(nullable: false),
                        Password = c.String(nullable: false, maxLength: 60),
                        OpenId = c.String(maxLength: 60),
                        CustomerRoleId = c.Int(nullable: false),
                        PasswordSalt = c.String(nullable: false, maxLength: 6),
                        LastModificationTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                        VerificationCode = c.String(maxLength: 10),
                        VerificatExpiryTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
