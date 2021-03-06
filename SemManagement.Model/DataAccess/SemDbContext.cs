﻿using Microsoft.EntityFrameworkCore;
using SemManagement.SemContext.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SemManagement.SemContext
{
    public class SemDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<SongStat> SongStats { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SongExtended> SongsDeleteds { get; set; }

        public DbSet<Playlist> Playlists { get; set; } 

        public DbSet<StationQueue> StationQueues { get; set; }

        public DbSet<StationsPlaylists> StationsPlaylists { get; set; }

        public DbSet<Stationsstatus> Stationsstatuses { get; set; }

        public DbSet<ScheduledStation> ScheduledStations { get; set; }

        public DbSet<Playlistssongs> Playlistssongs { get; set; }

        public SemDbContext(DbContextOptions<SemDbContext> options) : base(options)
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
            modelBuilder.Entity<StationsPlaylists>();
            modelBuilder.Entity<SongStat>();
            modelBuilder.Entity<Stationsstatus>().ToTable("Stationsstatus");
            modelBuilder.Entity<Playlistssongs>().ToTable("Playlistssongs");
        }
    }
}
