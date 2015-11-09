namespace DAL.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.SystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.SystemContext context)
        {
            context.UserAccounts.Add(new Entities.UserAccount()
            {
                Username = "Admin",
                Password = "24A88BB344FA5FCC490352794E12FEAF7BE50AB4", //test
                Salt = "wDDm2xLa3dzukknM6tu5c7FXrNI=",
                Claims = new List<UserClaim>() { 
                    new UserClaim() {
                        Type = System.Security.Claims.ClaimTypes.Email,
                        Value = "admin@test.com",
                    }, 
                    new UserClaim() {
                        Type = System.Security.Claims.ClaimTypes.Role,
                        Value = "SystemAdmin",
                    }
                }
            });
        }
    }
}
