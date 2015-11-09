using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SystemContext _ctx;

        public IUserAccountRepository UserAccountRepository { get; private set; }

        public EFUnitOfWork(SystemContext ctx)
        {
            _ctx = ctx;

            UserAccountRepository = new EFUserAccountRepository(ctx);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
