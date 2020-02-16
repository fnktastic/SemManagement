using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Local.Storage;
using SemManagement.UWP.Services.TagModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.TagModule.Service
{
    public interface ITagService
    {
        Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags);
        Task<List<Tag>> GetAllTagsAsync(int sid);
        Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags);
        Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId);
        Task<BoolResult> DeletePlaylistTagByIdAsync(int playlistId, Guid tagId);
        Task SavePlaylistTagRangeAsync(Model.Playlist playlist, List<Tag> tags);
        Task<List<Tag>> GetAllPlaylistTagsAsync(int plid);
    }

    public class TagService : ITagService
    {
        private readonly ITagProvider _tagProvider;

        public TagService(ITagProvider tagProvider)
        {
            _tagProvider = tagProvider;
        }

        public Task<BoolResult> DeletePlaylistTagByIdAsync(int playlistId, Guid tagId)
        {
            return _tagProvider.DeletePlaylistTagByIdAsync(playlistId, tagId);
        }

        public Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId)
        {
            return _tagProvider.DeleteStationTagByIdAsync(stationId, tagId);
        }

        public Task<List<Tag>> GetAllPlaylistTagsAsync(int plid)
        {
            return _tagProvider.GetAllPlaylistTagsAsync(plid);
        }

        public Task<List<Tag>> GetAllTagsAsync(int sid)
        {
            return _tagProvider.GetAllTagsAsync(sid);
        }

        public Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags)
        {
            return _tagProvider.GetStationByTagsAsync(tags);
        }

        public Task SavePlaylistTagRangeAsync(Model.Playlist playlist, List<Tag> tags)
        {
            return _tagProvider.SavePlaylistTagRangeAsync(playlist, tags);
        }

        public Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags)
        {
            return _tagProvider.SaveStationTagRangeAsync(station, tags);
        }
    }
}
