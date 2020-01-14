using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SemManagement.Local.Storage.DataAccess
{
    public class LocalStorageContext : DbContext
    {
        public LocalStorageContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Model.RuleDto> Rules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "semLocalStorage.db");
            options.UseSqlite("Data Source=" + dbPath);
        }
    }
}
