using Microsoft.EntityFrameworkCore;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.Model.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SongsDeleted> SongsDeleteds { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().ToTable("Songs");
            modelBuilder.Entity<Station>().ToTable("Stations");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<SongsDeleted>(); //.ToTable("SongsDeleted");
        }
    }
}
