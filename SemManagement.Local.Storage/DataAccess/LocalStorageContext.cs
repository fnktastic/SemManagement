using Microsoft.EntityFrameworkCore;
using SemManagement.Local.Storage.Model;
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
            Database.EnsureCreatedAsync().ConfigureAwait(false);
        }

        public DbSet<RuleDto> Rules { get; set; }

        public DbSet<RulePlaylistDto> RulePlaylists { get; set; }

        public DbSet<RuleStationDto> RuleStations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "semLocalStorage.db");
            options.UseSqlite("Data Source=" + dbPath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RuleDto>().HasKey(a => a.Id);
            modelBuilder.Entity<StationDto>().HasKey(a => a.Sid);
            modelBuilder.Entity<PlaylistDto>().HasKey(a => a.Plid);
            modelBuilder.Entity<TagDto>().HasKey(a => a.Id);

            modelBuilder.Entity<RuleStationDto>()
                .HasKey(pa => new { pa.RuleId, pa.StationId });

            modelBuilder.Entity<RulePlaylistDto>()
                .HasKey(pa => new { pa.RuleId, pa.PlaylistId });

            modelBuilder.Entity<StationPlaylistDto>()
                .HasKey(pa => new { pa.StationId, pa.PlaylistId });

            modelBuilder.Entity<PlaylistTagDto>()
                .HasKey(pa => new { pa.PlaylistId, pa.TagId });

            modelBuilder.Entity<RuleStationDto>()
                .HasOne(x => x.Rule)
                .WithMany(p => p.RuleStations)
                .HasForeignKey(pc => pc.RuleId);

            modelBuilder.Entity<RulePlaylistDto>()
                .HasOne(x => x.Rule)
                .WithMany(p => p.RulePlaylists)
                .HasForeignKey(pc => pc.RuleId);

            modelBuilder.Entity<RulePlaylistDto>()
                .HasOne(x => x.Playlist)
                .WithMany(p => p.RulePlaylists)
                .HasForeignKey(pc => pc.PlaylistId);

            modelBuilder.Entity<PlaylistTagDto>()
                .HasOne(pc => pc.Tag)
                .WithMany(c => c.PlaylistTags)
                .HasForeignKey(pc => pc.TagId);

            modelBuilder.Entity<PlaylistTagDto>()
                .HasOne(pc => pc.Playlist)
                .WithMany(c => c.PlaylistTags)
                .HasForeignKey(pc => pc.PlaylistId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
