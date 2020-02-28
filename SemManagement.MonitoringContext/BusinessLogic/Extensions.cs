using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemManagement.MonitoringContext.BusinessLogic
{
    public static class Extensions
    {
        public static IFeedItem ToFeedItem(this PlaylistSnapshotDto playlistSnapshotDto)
        {
            return new PalylistFeedItem()
            {
                DateTime = playlistSnapshotDto.DateTime,
                MonitorType = MonitorTypeEnum.Playlists,
                Plid = playlistSnapshotDto.PlaylistId,
                Message = $"{playlistSnapshotDto.PlaylistName} (ID: {playlistSnapshotDto.PlaylistId})" 
            };
        }

        public static IList<IFeedItem> ToFeedItems(this ICollection<PlaylistSnapshotSongDto> playlistSnapshotSongDtos)
        {
            return playlistSnapshotSongDtos.Select(x => x.ToFeedItem()).ToList();
        }

        public static IFeedItem ToFeedItem(this PlaylistSnapshotSongDto playlistSnapshotSongDto)
        {
            return new SongFeedItem()
            {
                DateTime = playlistSnapshotSongDto.DateTime,
                MonitorType = MonitorTypeEnum.Songs,
                Sgid  = playlistSnapshotSongDto.SongId,
                Message = $"{playlistSnapshotSongDto.SongName} (ID: {playlistSnapshotSongDto.SongId})"
            };
        }

        public static IFeedItem ToFeedItem(this StationSnapshotPlaylistDto stationSnapshotPlaylistDto)
        {
            return new StationPlaylistFeedItem()
            {
                DateTime = stationSnapshotPlaylistDto.DateTime,
                MonitorType = MonitorTypeEnum.Stations,
                Plid = stationSnapshotPlaylistDto.PlaylistId,
                Message = $"{stationSnapshotPlaylistDto.PlaylistName} (ID: {stationSnapshotPlaylistDto.PlaylistId})"
            };
        }
        public static IList<IFeedItem> ToFeedItems(this ICollection<StationSnapshotPlaylistDto> stationSnapshotPlaylistDtos)
        {
            return stationSnapshotPlaylistDtos.Select(x => x.ToFeedItem()).ToList();
        }
    }
}
