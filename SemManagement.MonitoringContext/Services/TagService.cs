using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface ITagService
    {
        Task SaveStationTagRangeAsync(StationDto station, List<TagDto> tags);

        Task<List<TagDto>> GetAllTagsAsync(int sid);

        Task<List<StationDto>> GetStationByTagsAsync(List<TagDto> tags);

        Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId);
    }

    public class TagService : ITagService
    {
        private readonly ILocalStationRepository _stationRepository;
        private readonly ILocalTagRepository _tagRepository;

        public TagService(ILocalStationRepository stationRepository, ILocalTagRepository tagRepository)
        {
            _stationRepository = stationRepository;
            _tagRepository = tagRepository;
        }

        public async Task SaveStationTagRangeAsync(StationDto station, List<TagDto> tags)
        {
            await _stationRepository.SaveAsync(station);

            var savedTags = await _tagRepository.SaveRangeAsync(tags);

            var stationTags = savedTags.Select(x => new StationTagDto()
            {
                Sid = station.Sid,
                TagId = x.Id,
            }).ToList();

            await _tagRepository.SaveRangeAsync(stationTags);
        }

        public async Task<List<TagDto>> GetAllTagsAsync(int sid)
        {
            var tags = await _tagRepository.GetAllAsync(sid);

            return tags;
        }

        public async Task<List<StationDto>> GetStationByTagsAsync(List<TagDto> tags)
        {
            var stationsByTags = await _tagRepository.GetStationByTagsAsync(tags);

            return stationsByTags;
        }

        public async Task<BoolResult> DeleteStationTagByIdAsync(int stationId, Guid tagId)
        {
            return await _tagRepository.DeleteByStationTagIdAsync(stationId, tagId);
        }
    }
}
