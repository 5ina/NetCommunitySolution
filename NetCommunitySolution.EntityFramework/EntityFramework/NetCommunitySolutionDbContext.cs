using System.Data.Common;
using Abp.EntityFramework;
using NetCommunitySolution.Domain.Settings;
using System.Data.Entity;

namespace NetCommunitySolution.EntityFramework
{
    public class NetCommunitySolutionDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public virtual IDbSet<Setting> Users { get; set; }
        public NetCommunitySolutionDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in NetCommunitySolutionDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of NetCommunitySolutionDbContext since ABP automatically handles it.
         */
        public NetCommunitySolutionDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public NetCommunitySolutionDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public NetCommunitySolutionDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
