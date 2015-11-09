using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserClaim
    {
        public int UserClaimId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
