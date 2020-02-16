using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Repository
{
    public interface ILocalTagRepository
    {
        Task<List<TagDto>> SaveRangeAsync(List<TagDto> tags);

        Task SaveRangeAsync(List<StationTagDto> stationTags);

        Task SaveRangeAsync(List<PlaylistTagDto> playlistTags);

        Task<List<TagDto>> GetAllAsync(int sid);

        Task<List<StationDto>> GetStationByTagsAsync(List<TagDto> tags);

        Task<BoolResult> DeleteByStationTagIdAsync(int stationId, Guid tagId);

        Task<List<TagDto>> GetAllPlaylisTagsAsync(int plid);

        Task<BoolResult> DeleteByPlaylistTagIdAsync(int playlistId, Guid tagId);

        Task<List<PlaylistDto>> GetPlaylistByTagsAsync(List<TagDto> tags);
    }

    public class LocalTagRepository : ILocalTagRepository
    {
        private MonitoringDbContext _context;

        public LocalTagRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task<List<TagDto>> GetAllAsync(int sid)
        {
            return await _context.StationTags.Where(x => x.Sid == sid)
                .Include(x => x.Tag)
                .Select(x => x.Tag)
                .ToListAsync();
        }

        public async Task<List<TagDto>> GetAllPlaylisTagsAsync(int plid)
        {
            return await _context.PlaylistTags.Where(x => x.Plid == plid)
                .Include(x => x.Tag)
                .Select(x => x.Tag)
                .ToListAsync();
        }

        public async Task<List<PlaylistDto>> GetPlaylistByTagsAsync(List<TagDto> tags)
        {
            var tagNames = tags.Select(x => x.Name).ToList();

            var playlists = await _context.PlaylistTags
                .Include(x => x.Tag)
                .Where(x => tagNames.Contains(x.Tag.Name))
                .Select(x => x.Playlist)
                .ToListAsync();

            return playlists;
        }

        public async Task<BoolResult> DeleteByStationTagIdAsync(int stationId, Guid tagId)
        {
            var stationTags = await _context.StationTags.Where(x => x.Sid == stationId && x.TagId == tagId).ToListAsync();

            if(stationTags != null && stationTags.Count > 0)
            {
                var stationTag = stationTags.First();

                _context.Entry<StationTagDto>(stationTag).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                return await Task.FromResult<BoolResult>(new BoolResult(true));
            }

            return await Task.FromResult<BoolResult>(new BoolResult(false));
        }

        public async Task<BoolResult> DeleteByPlaylistTagIdAsync(int playlistId, Guid tagId)
        {
            var playlistTags = await _context.PlaylistTags.Where(x => x.Plid == playlistId && x.TagId == tagId).ToListAsync();

            if (playlistTags != null && playlistTags.Count > 0)
            {
                var playlistTag = playlistTags.First();

                _context.Entry<PlaylistTagDto>(playlistTag).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                return await Task.FromResult<BoolResult>(new BoolResult(true));
            }

            return await Task.FromResult<BoolResult>(new BoolResult(false));
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
    }
}
