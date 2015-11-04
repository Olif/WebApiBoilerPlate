using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// This is an example of a useraccount repository. 
    /// </summary>
    public class UserAccountRepository
    {
        public UserAccountRepository()
        {

        }

        public async Task<UserAccount> GetAsync(string userName)
        {
            return await Task.FromResult(new UserAccount() { 
                Username = "fa-admin",
                Password = "pass"
            }); 
        }

        public UserAccount Get(string username)
        {
            return this.GetAsync(username).Result;
        }
    }
}
