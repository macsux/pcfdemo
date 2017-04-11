using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;

namespace FortuneCookieDatabase
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class FortuneCookieDbContext : DbContext
    {

        public FortuneCookieDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FortuneCookieDbContext, Configuration>(useSuppliedContext: true));
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FortuneCookie>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FortuneCookie> FortuneCookies { get; set; }

    }
}
