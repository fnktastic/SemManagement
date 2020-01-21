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

        public DbSet<StationTagDto> StationTags { get; set; }

        public DbSet<PlaylistTagDto> PlaylistTags { get; set; }

        public DbSet<TagDto> Tags { get; set; }

        public DbSet<StationDto> Stations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "semLocalStorage.db");
            options.UseSqlite(string.Format("Data Source={0};", dbPath));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RuleDto>().HasKey(a => a.Id);
            modelBuilder.Entity<StationDto>().HasKey(a => a.Sid);
            modelBuilder.Entity<PlaylistDto>().HasKey(a => a.Plid);
            modelBuilder.Entity<TagDto>().HasKey(a => a.Id);

            modelBuilder.Entity<RuleStationDto>()
                .HasKey(pa => pa.RuleStationId);

            modelBuilder.Entity<RulePlaylistDto>()
                .HasKey(pa => pa.RulePlaylistId);

            modelBuilder.Entity<StationPlaylistDto>()
                .HasKey(pa => pa.StationPlaylistId);

            modelBuilder.Entity<StationTagDto>()
                .HasKey(pa => new { pa.StationId, pa.TagId });

            modelBuilder.Entity<PlaylistTagDto>()
                .HasKey(pa => new { pa.PlaylistId, pa.TagId });

            modelBuilder.Entity<RuleStationDto>()
                .HasOne(x => x.Rule)
                .WithMany(p => p.RuleStations)
                .HasForeignKey(pc => pc.RuleId)
                .IsRequired();

            modelBuilder.Entity<RulePlaylistDto>()
                .HasOne(x => x.Rule)
                .WithMany(p => p.RulePlaylists)
                .HasForeignKey(pc => pc.RuleId)
                .IsRequired();

            modelBuilder.Entity<RulePlaylistDto>()
                .HasOne(x => x.Playlist)
                .WithMany(p => p.RulePlaylists)
                .HasForeignKey(pc => pc.PlaylistId)
                .IsRequired();

            modelBuilder.Entity<StationPlaylistDto>()
                .HasOne(pc => pc.Playlist)
                .WithMany(c => c.StationPlaylists)
                .HasForeignKey(pc => pc.PlaylistId)
                .IsRequired();

            modelBuilder.Entity<StationPlaylistDto>()
                .HasOne(pc => pc.Station)
                .WithMany(c => c.StationPlaylists)
                .HasForeignKey(pc => pc.StationId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
