namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Value = c.String(),
                        UserAccount_UserAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.UserAccounts", t => t.UserAccount_UserAccountId, cascadeDelete: true)
                .Index(t => t.UserAccount_UserAccountId);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserAccountId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaims", "UserAccount_UserAccountId", "dbo.UserAccounts");
            DropIndex("dbo.UserClaims", new[] { "UserAccount_UserAccountId" });
            DropTable("dbo.UserAccounts");
            DropTable("dbo.UserClaims");
        }
    }
}
