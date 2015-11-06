using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SystemContext : DbContext
    {
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        public SystemContext() 
            : this("Debug", new CreateDatabaseIfNotExists<SystemContext>())
        {

        }

        public SystemContext(string name, IDatabaseInitializer<SystemContext> initializer) 
            : base(name)
        {
            Database.SetInitializer(initializer);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
