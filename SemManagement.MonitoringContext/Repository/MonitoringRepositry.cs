﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.BusinessLogic;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using SemManagement.SemContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Repository
{
    public interface IMonitoringRepositry
    {
        Task<List<StationMonitoringDto>> GetMonitoredStations();

        Task AddMonitoringStation(StationMonitoringDto stationMonitoring);

        Task SaveStationSnapshots(List<StationSnapshotDto> stationSnapshots);

        Task SaveStationSnapshotPlaylists(List<StationSnapshotPlaylistDto> stationSnapshotPlaylists);

        Task SavePlaylistSnapshots(List<PlaylistSnapshotDto> playlistSnapshots);

        Task SavePlaylistSnapshotSongs(List<PlaylistSnapshotSongDto> playlistSnapshots);

        Task<StationMonitoringDto> CreateIfNotExist(StationMonitoringDto stationMonitoring = null, int? sid = null);

        Task<List<PlaylistMonitoringDto>> GetMonitoredPlaylists();

        Task SavePlaylistsMonitrorings(List<PlaylistMonitoringDto> playlistMonitorings);

        Task SaveStationPlayerStateRangeAsync(List<StationPlayerStateDto> stationsPlayerState);

        Task<FeedList> GetQucikMonitoringForStaton(List<int> plids, int sid);

        Task<FeedList> GetQucikMonitoring(DateTime dateTime);
    }

    public class MonitoringRepositry : IMonitoringRepositry
    {
        private readonly MonitoringDbContext _monitoringDbContext;
        private readonly SemDbContext _semDbContext;

        public MonitoringRepositry(MonitoringDbContext monitoringDbContext, SemDbContext semDbContext)
        {
            _monitoringDbContext = monitoringDbContext;
            _semDbContext = semDbContext;
        }

        public async Task AddMonitoringStation(StationMonitoringDto stationMonitoring)
        {
            _monitoringDbContext.StationMonitorings.Add(stationMonitoring);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task<StationMonitoringDto> CreateIfNotExist(StationMonitoringDto stationMonitoring = null, int? sid = null)
        {
            int stationId = -1;

            if (stationMonitoring != null)
                stationId = stationMonitoring.StationId;

            if (sid.HasValue)
                stationId = sid.Value;

            var items = await _monitoringDbContext.StationMonitorings.Where(x => x.StationId == stationId).ToListAsync();

            if (items.Count > 0) return items.First();

            var station = await _semDbContext.Stations.FirstOrDefaultAsync(x => x.Sid == stationId);

            var newlyCreated = new StationMonitoringDto()
            {
                Id = Guid.NewGuid(),
                RepeatInterval = 1,
                WantedAmountOfUpdates = 15,
                StationId = station.Sid,
                StationName = station.Name,
                IsAutoGenerated = true
            };

            _monitoringDbContext.StationMonitorings.Add(newlyCreated);

            return newlyCreated;
        }

        public async Task<List<PlaylistMonitoringDto>> GetMonitoredPlaylists()
        {
            return await _monitoringDbContext.PlaylistMonitorings.ToListAsync();
        }

        public async Task<List<StationMonitoringDto>> GetMonitoredStations()
        {
            return await _monitoringDbContext.StationMonitorings.ToListAsync();
        }

        public async Task SavePlaylistSnapshots(List<PlaylistSnapshotDto> playlistSnapshots)
        {
            _monitoringDbContext.PlaylistSnapshots.AddRange(playlistSnapshots);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task SavePlaylistSnapshotSongs(List<PlaylistSnapshotSongDto> playlistSnapshots)
        {
            _monitoringDbContext.PlaylistSnapshotSongs.AddRange(playlistSnapshots);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task SaveStationSnapshotPlaylists(List<StationSnapshotPlaylistDto> stationSnapshotPlaylists)
        {
            _monitoringDbContext.StationSnapshotPlaylists.AddRange(stationSnapshotPlaylists);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task SaveStationSnapshots(List<StationSnapshotDto> stationSnapshots)
        {
            _monitoringDbContext.StationSnapshots.AddRange(stationSnapshots);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task SavePlaylistsMonitrorings(List<PlaylistMonitoringDto> playlistMonitorings)
        {
            await _monitoringDbContext.BulkInsertOrUpdateAsync(playlistMonitorings);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task SaveStationPlayerStateRangeAsync(List<StationPlayerStateDto> stationsPlayerState)
        {
            await _monitoringDbContext.BulkInsertAsync(stationsPlayerState);

            await _monitoringDbContext.SaveChangesAsync();
        }

        public async Task<FeedList> GetQucikMonitoringForStaton(List<int> plids, int sid)
        {
            var feedList = new FeedList();

            var modifiedPlayistsSnapshots = await _monitoringDbContext
                .PlaylistSnapshots
                .Where(x => plids.Contains(x.PlaylistId))
                .Include(x => x.SnapshotSongs)
                .ToListAsync();

            foreach (var stationPlayistsSnapshot in modifiedPlayistsSnapshots)
            {
                var playlistFeedItem = stationPlayistsSnapshot.ToFeedItem();

                if (stationPlayistsSnapshot.SnapshotAction != SnapshotActionEnum.None)
                    feedList.Add(playlistFeedItem);

                var songsFeedItems = stationPlayistsSnapshot.SnapshotSongs.ToFeedItems(stationPlayistsSnapshot.PlaylistName);

                feedList.AddRange(songsFeedItems);
            }


            var stationSnapshots = await _monitoringDbContext
                .StationSnapshots
                .Where(x => x.StationId == sid)
                .Include(x => x.SnapshotPlaylists)
                .ToListAsync();

            foreach (var stationSnapshot in stationSnapshots)
            {
                var stationPlaylistFeedItems = stationSnapshot.SnapshotPlaylists.ToFeedItems();

                feedList.AddRange(stationPlaylistFeedItems);
            }

            return feedList;
        }

        public async Task<FeedList> GetQucikMonitoring(DateTime dateTime)
        {
            var feedList = new FeedList();

            var modifiedPlayistsSnapshots = await _monitoringDbContext
                .PlaylistSnapshots
                .Where(x => x.DateTime > dateTime)
                .ToListAsync();

            foreach (var stationPlayistsSnapshot in modifiedPlayistsSnapshots)
            {
                var playlistFeedItem = stationPlayistsSnapshot.ToFeedItem();

                if (stationPlayistsSnapshot.SnapshotAction != SnapshotActionEnum.None)
                    feedList.Add(playlistFeedItem);
            }



            var stationSnapshotsSongs = await _monitoringDbContext
                .PlaylistSnapshotSongs
                .Include(x => x.PlaylistSnapshot)
                .Where(x => x.DateTime > dateTime)
                .ToListAsync();

            var stationPlaylistFeedItems = stationSnapshotsSongs.ToCollection().ToFeedItems();

            feedList.AddRange(stationPlaylistFeedItems);



            var stationSnapshotPlaylists = await _monitoringDbContext
                .StationSnapshotPlaylists
                .Where(x => x.DateTime > dateTime)
                .Include(x => x.StationSnapshot)
                .ToListAsync();

            var stationSnapshotPlaylistsFeedItems = stationSnapshotPlaylists.ToFeedItems();

            feedList.AddRange(stationSnapshotPlaylistsFeedItems);

            return feedList;
        }
    }
}
