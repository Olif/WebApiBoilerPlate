using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EFUserAccountRepository : IUserAccountRepository
    {
        private readonly SystemContext _ctx;

        public EFUserAccountRepository(SystemContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<UserAccount> Find()
        {
            return _ctx.UserAccounts.ToList();
        }

        public UserAccount Get(int id)
        {
            return _ctx.UserAccounts.Find(id);
        }

        public UserAccount Get(string userName)
        {
            return _ctx.UserAccounts.FirstOrDefault(x => x.Username == userName);
        }

        public void Add(UserAccount userAccount)
        {
            _ctx.UserAccounts.Add(userAccount);
        }
    }
}
