using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IUserAccountRepository
    {
        IEnumerable<UserAccount> Find();

        UserAccount Get(int id);
        UserAccount Get(string userName);

        void Add(UserAccount userAccount);
    }
}
