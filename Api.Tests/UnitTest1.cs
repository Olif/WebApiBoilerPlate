using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Security;

namespace Api.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var salt = PasswordHelper.GenerateSalt(20);
            var hashedPassword = PasswordHelper.HashPassword("test", salt);
        }
    }
}
