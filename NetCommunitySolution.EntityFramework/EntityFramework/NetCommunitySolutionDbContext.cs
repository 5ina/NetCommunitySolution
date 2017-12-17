using System.Data.Common;
using Abp.EntityFramework;
using NetCommunitySolution.Domain.Settings;
using System.Data.Entity;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Catalog;

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
        public virtual IDbSet<Setting> Setting { get; set; }

        //Customer
        public virtual IDbSet<Customer> Customer { get; set; }
        public virtual IDbSet<CustomerAttribute> CustomerAttribute { get; set; }
        public virtual IDbSet<CustomerFollow> CustomerFollow { get; set; }
        
        //Catalog
        public virtual IDbSet<Category> Category { get; set; }
        public virtual IDbSet<ContentLabel> ContentLabel { get; set; }
        public virtual IDbSet<Post> Post { get; set; }
        public virtual IDbSet<PostLabel> PostLabel { get; set; }
        public virtual IDbSet<PostComment> PostComment { get; set; }


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
