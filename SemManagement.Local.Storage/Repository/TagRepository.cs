using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SemManagement.LocalContext.DataAccess;
using SemManagement.LocalContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.LocalContext.Repository
{
    public interface ITagRepository
    {
        Task<List<TagDto>> SaveRangeAsync(List<TagDto> tags);

        Task SaveRangeAsync(List<StationTagDto> stationTags);

        Task SaveRangeAsync(List<PlaylistTagDto> playlistTags);

        Task<List<TagDto>> GetAllAsync(int sid);

        Task<List<StationDto>> GetStationByTagsAsync(List<TagDto> tags);
    }

    public class TagRepository : ITagRepository
    {
        private LocalDbContext _context;

        public TagRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<List<TagDto>> GetAllAsync(int sid)
        {
            return await _context.StationTags.Where(x => x.StationId == sid)
                .Include(x => x.Tag)
                .Select(x => x.Tag)
                .ToListAsync();
        }

        public async Task<List<StationDto>> GetStationByTagsAsync(List<TagDto> tags)
        {
            var tagNames = tags.Select(x => x.Name).ToList();

            var stations = await _context.StationTags
                .Include(x => x.Tag)
                .Where(x => tagNames.Contains(x.Tag.Name))
                .Select(x => x.Station)
                .ToListAsync();

            return stations;
        }

        public async Task<List<TagDto>> SaveRangeAsync(List<TagDto> tags)
        {
            var savedTags = new List<TagDto>();

            foreach (var tag in tags)
            {
                var existedTag = await _context.Tags.FirstOrDefaultAsync(x => x.Name.ToUpper() == tag.Name.ToUpper());

                if (existedTag != null)
                {
                    savedTags.Add(existedTag);
                    continue;
                }

                tag.Id = Guid.NewGuid();
                _context.Add(tag);
                savedTags.Add(tag);
            }

            await _context.SaveChangesAsync();

            return savedTags;
        }

        public async Task SaveRangeAsync(List<StationTagDto> stationTags)
        {
            await _context.BulkInsertOrUpdateAsync(stationTags);

            await _context.SaveChangesAsync();
        }

        public async Task SaveRangeAsync(List<PlaylistTagDto> playlistTags)
        {
            await _context.BulkInsertOrUpdateAsync(playlistTags);

            await _context.SaveChangesAsync();
        }
    }
}
