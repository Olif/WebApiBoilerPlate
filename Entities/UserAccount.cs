using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserAccount
    {
        private ICollection<UserClaim> _claims;

        public int UserAccountId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public virtual ICollection<UserClaim> Claims {
            get { return _claims ?? (_claims = new List<UserClaim>()); }
            set { _claims = value; }
        }
    }
}
