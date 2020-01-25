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

        public DbSet<StationMonitoring> StationMonitorings { get; set; }

        public DbSet<StationSnapshot> StationSnapshots { get; set; }

        public DbSet<StationSnapshotPlaylist> StationSnapshotPlaylists { get; set; }

        public DbSet<PlaylistSnapshot> PlaylistSnapshots { get; set; }

        public DbSet<PlaylistSnapshotSong> PlaylistSnapshotSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
