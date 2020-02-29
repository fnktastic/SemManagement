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
                SnapshotAction = playlistSnapshotDto.SnapshotAction,
                Parent = "",
                Message = $"{playlistSnapshotDto.PlaylistName}" 
            };
        }


        public static IFeedItem ToFeedItem(this PlaylistSnapshotSongDto playlistSnapshotSongDto, string playlistName)
        {
            return new SongFeedItem()
            {
                DateTime = playlistSnapshotSongDto.DateTime,
                MonitorType = MonitorTypeEnum.Songs,
                Sgid  = playlistSnapshotSongDto.SongId,
                SnapshotAction = playlistSnapshotSongDto.SnapshotAction,
                Parent = playlistName,
                Message = $"{playlistSnapshotSongDto.SongName}"
            };
        }

        public static IFeedItem ToFeedItem(this StationSnapshotPlaylistDto stationSnapshotPlaylistDto)
        {
            return new StationPlaylistFeedItem()
            {
                DateTime = stationSnapshotPlaylistDto.DateTime,
                MonitorType = MonitorTypeEnum.Playlists,
                Plid = stationSnapshotPlaylistDto.PlaylistId,
                SnapshotAction = stationSnapshotPlaylistDto.SnapshotAction,
                Parent = "",
                Message = $"{stationSnapshotPlaylistDto.PlaylistName}"
            };
        }
        public static IList<IFeedItem> ToFeedItems(this ICollection<StationSnapshotPlaylistDto> stationSnapshotPlaylistDtos)
        {
            return stationSnapshotPlaylistDtos.Select(x => x.ToFeedItem()).ToList();
        }

        public static IList<IFeedItem> ToFeedItems(this ICollection<PlaylistSnapshotSongDto> playlistSnapshotSongDtos, string playlistName)
        {
            return playlistSnapshotSongDtos.Select(x => x.ToFeedItem(playlistName)).ToList();
        }
    }
}
