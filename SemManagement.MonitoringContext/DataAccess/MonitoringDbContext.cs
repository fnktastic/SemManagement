using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.MonitoringContext.DataAccess
{
    public class MonitoringDbContext : DbContext
    {
        public MonitoringDbContext(DbContextOptions<MonitoringDbContext> options) : base(options)
        {

        }

        public DbSet<StationMonitoringDto> StationMonitorings { get; set; }

        public DbSet<PlaylistMonitoringDto> PlaylistMonitorings { get; set; }

        public DbSet<StationSnapshotDto> StationSnapshots { get; set; }

        public DbSet<StationSnapshotPlaylistDto> StationSnapshotPlaylists { get; set; }

        public DbSet<PlaylistSnapshotDto> PlaylistSnapshots { get; set; }

        public DbSet<PlaylistSnapshotSongDto> PlaylistSnapshotSongs { get; set; }

        public DbSet<RuleDto> Rules { get; set; }

        public DbSet<RulePlaylistDto> RulePlaylists { get; set; }

        public DbSet<RuleStationDto> RuleStations { get; set; }

        public DbSet<StationTagDto> StationTags { get; set; }

        public DbSet<PlaylistTagDto> PlaylistTags { get; set; }

        public DbSet<TagDto> Tags { get; set; }

        public DbSet<StationDto> Stations { get; set; }

        public DbSet<RuleLogDto> RuleLogs { get; set; }

        public DbSet<MonitoringDto> Monitorings { get; set; }

        public DbSet<PlaylistDto> Playlists { get; set; }

        public DbSet<StationPlayerStateDto> StationPlayerStates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RuleLogDto>().HasKey(a => a.Id);

            modelBuilder.Entity<StationMonitoringDto>().HasKey(a => a.Id);

            modelBuilder.Entity<StationMonitoringDto>()
                .HasMany(x => x.Snapshots);

            modelBuilder.Entity<PlaylistMonitoringDto>().HasKey(a => a.Id);

            modelBuilder.Entity<PlaylistMonitoringDto>().Property(a => a.Id).ValueGeneratedNever();

            modelBuilder.Entity<PlaylistMonitoringDto>()
                .HasMany(x => x.Snapshots);

            modelBuilder.Entity<RuleDto>().HasKey(a => a.Id);

            modelBuilder.Entity<StationPlayerStateDto>().HasKey(a => a.Id);

            modelBuilder.Entity<StationDto>(x =>
            {
                x.HasKey(a => a.Sid);
                x.Property(e => e.Sid).ValueGeneratedNever();
            });
            modelBuilder.Entity<PlaylistDto>(x =>
            {
                x.HasKey(a => a.Plid);
                x.Property(e => e.Plid).ValueGeneratedNever();
            });
            modelBuilder.Entity<TagDto>().HasKey(a => a.Id);

            modelBuilder.Entity<RuleStationDto>()
                .HasKey(pa => pa.RuleStationId);

            modelBuilder.Entity<RulePlaylistDto>()
                .HasKey(pa => pa.RulePlaylistId);

            modelBuilder.Entity<StationPlaylistDto>()
                .HasKey(pa => pa.StationPlaylistId);

            modelBuilder.Entity<StationTagDto>()
                .HasKey(pa => new { pa.Sid, pa.TagId });

            modelBuilder.Entity<PlaylistTagDto>()
                .HasKey(pa => new { pa.Plid, pa.TagId });

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

    public static class DbInitializer
    {
        public static void Initialize(MonitoringDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
