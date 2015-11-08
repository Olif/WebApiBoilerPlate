using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Entities;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;

namespace DAL.IntegrationTests
{
    [TestClass]
    public class UserAccountRepositoryTests
    {
        public SystemContext GetContext()
        {
            return new SystemContext("WebApiBoilerPlateDebug", new DropCreateDatabaseAlways<SystemContext>());
        }

        public IUnitOfWork GetUOW()
        {
            return new EFUnitOfWork(GetContext());
        }

        [TestMethod]
        public void UserAccountRepository_Can_Add_Find_And_GetUsers()
        {
            var newAccount = new UserAccount()
            {
                Username = "Test",
                Password = "pass",
                Salt = "salt",
                Claims = new List<Entities.Claim>() { 
                    new Entities.Claim() {
                        Type = ClaimTypes.Role,
                        Value = "Admin"
                    },
                    new Entities.Claim() {
                        Type = ClaimTypes.Role,
                        Value = "SysAdmin"
                    }
                }
            };

            // Can add new user account
            using (var uow = GetUOW())
            {
                uow.UserAccountRepository.Add(newAccount);
                uow.SaveChanges();
            }

            // Can find all user accounts
            using (var uow = GetUOW())
            {
                var users = uow.UserAccountRepository.Find();
                Assert.AreEqual(1, users.Count());
            }

            // Can get user by id
            using (var uow = GetUOW())
            {
                var user = uow.UserAccountRepository.Get(1);
                Assert.IsNotNull(user);
            }

            // Can get user by username
            using (var uow = GetUOW())
            {
                var user = uow.UserAccountRepository.Get("Test");
                Assert.IsNotNull(user);
            }
        }
    }
}
