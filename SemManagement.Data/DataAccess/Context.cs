using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using SemManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.Data.DataAccess
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Context : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<User> Users { get; set; }

        public Context() : base("SemContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Song>().MapToStoredProcedures();
            modelBuilder.Entity<Station>().MapToStoredProcedures();
            modelBuilder.Entity<User>().MapToStoredProcedures();
        }
    }
}
