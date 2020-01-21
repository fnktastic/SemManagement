using Microsoft.EntityFrameworkCore;
using SemManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.Model.DataAccess
{
    public class SemContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SongExtended> SongsDeleteds { get; set; }

        public DbSet<Playlist> Playlists { get; set; } 

        public DbSet<StationQueue> StationQueues { get; set; }

        public SemContext(DbContextOptions<SemContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().ToTable("Songs");
            modelBuilder.Entity<Station>().ToTable("Stations");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Playlist>().ToTable("Playlists");
            modelBuilder.Entity<SongExtended>();
            modelBuilder.Entity<StationQueue>();
        }
    }
}
