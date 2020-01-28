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
    }

    public class TagService : ITagService
    {
        private readonly ITagProvider _tagProvider;

        public TagService(ITagProvider tagProvider)
        {
            _tagProvider = tagProvider;
        }

        public Task<List<Tag>> GetAllTagsAsync(int sid)
        {
            return _tagProvider.GetAllTagsAsync(sid);
        }

        public Task<List<Model.Station>> GetStationByTagsAsync(List<Tag> tags)
        {
            return _tagProvider.GetStationByTagsAsync(tags);
        }

        public Task SaveStationTagRangeAsync(Model.Station station, List<Tag> tags)
        {
            return _tagProvider.SaveStationTagRangeAsync(station, tags);
        }
    }
}
