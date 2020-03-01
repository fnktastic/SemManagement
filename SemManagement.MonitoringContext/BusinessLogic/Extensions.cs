using SemManagement.MonitoringContext.Enum;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                Parent = $"{stationSnapshotPlaylistDto.StationSnapshot.StationId}",
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

        public static IList<IFeedItem> ToFeedItems(this ICollection<PlaylistSnapshotSongDto> playlistSnapshotSongDtos)
        {
            return playlistSnapshotSongDtos.Select(x => x.ToFeedItem(x.PlaylistSnapshot.PlaylistName)).ToList();
        }
    }

    public static class CollectionExtensions
    {
        public static Collection<T> ToCollection<T>(this List<T> items)
        {
            Collection<T> collection = new Collection<T>();

            for (int i = 0; i < items.Count; i++)
            {
                collection.Add(items[i]);
            }

            return collection;
        }
    }
}
