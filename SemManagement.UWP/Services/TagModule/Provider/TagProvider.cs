using SemManagement.UWP.Configurations;
using SemManagement.UWP.Helper.TransportModel;
using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.TagModule.Provider
{
    public interface ITagProvider
    {
        Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags);
        Task<List<Tag>> GetAllTagsAsync(int sid);
        Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags);
        Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId);
        Task SavePlaylistTagRangeAsync(Model.Playlist playlist, List<Tag> tags);
        Task<List<Tag>> GetAllPlaylistTagsAsync(int plid);
        Task<BoolResult> DeletePlaylistTagByIdAsync(int playlistId, Guid tagId);
    }

    public class TagProvider : WebApiProvider, ITagProvider
    {

        public TagProvider(IRestEndpoints restEndpoints, PublicApiConfiguration settings) : base(restEndpoints, settings)
        {

        }

        public Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "saveStationTagRangeAsync");

            var tagsTransportModel = new TagTransportModel(station, tags);

            return AddAsync<TagTransportModel>(endpoint, tagsTransportModel);
        }

        public Task<List<Tag>> GetAllTagsAsync(int sid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "getAllTagsAsync");

            return GetAllTagsAsync<Tag>(endpoint, sid);
        }

        public Task<List<Tag>> GetAllPlaylistTagsAsync(int plid)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "getAllPlaylisTagsAsync");

            return GetAllPlaylistTagsAsync<Tag>(endpoint, plid);
        }

        public Task SavePlaylistTagRangeAsync(Model.Playlist playlist, List<Tag> tags)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "savePlaylistTagRangeAsync");

            var tagsTransportModel = new TagTransportModel(playlist, tags);

            return AddAsync<TagTransportModel>(endpoint, tagsTransportModel);
        }

        public Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "getStationByTagsAsync");

            var tagsTransportModel = new TagTransportModel(tags);

            return GetStationByTagsAsync<Model.Station>(endpoint, tagsTransportModel);
        }

        public Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "deleteStationTagByIdAsync");

            return DeleteStationTagByIdAsync<BoolResult>(endpoint, stationId, tagId);
        }

        public Task<BoolResult> DeletePlaylistTagByIdAsync(int playlistId, Guid tagId)
        {
            string endpoint = string.Format("{0}/{1}", RestEndpoint.Tags, "deletePlaylistTagByIdAsync");

            return DeletePlaylistTagByIdAsync<BoolResult>(endpoint, playlistId, tagId);
        }
    }
}
