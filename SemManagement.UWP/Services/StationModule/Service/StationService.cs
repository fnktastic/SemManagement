using SemManagement.UWP.Model;
using SemManagement.UWP.Services.StationModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.StationModule.Service
{
    public interface IStationService
    {
        Task<List<Station>> TakeAsync(int take, int skip = 0);
        Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId);
        Task<List<Song>> GetStationSongsAsync(int stationId);
        Task<User> GetStationUserAsync(int stationId);


        Task<List<Station>> GetFakeDataAsync();
        Task<List<SongsDeleted>> GetFakeDeletedSongsAsync(int stationId);

    }
    public class StationService : IStationService
    {
        private readonly IStationProvider _stationProvider;

        public StationService(IStationProvider stationProvider)
        {
            _stationProvider = stationProvider;
        }

        public async Task<List<SongsDeleted>> GetDeletedSongsAsync(int stationId)
        {
            return await _stationProvider.GetDeletedSongsAsync(stationId);
        }

        public async Task<List<Station>> TakeAsync(int take, int skip = 0)
        {
            return await _stationProvider.TakeAsync(take, skip);
        }

        public Task<List<Song>> GetStationSongsAsync(int stationId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetStationUserAsync(int stationId)
        {
            throw new NotImplementedException();
        }

        #region fake data
        public async Task<List<SongsDeleted>> GetFakeDeletedSongsAsync(int stationId)
        {
            await Task.Delay(3500);

            return new List<SongsDeleted>()
            {
                new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life", 
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                 new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                  new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                   new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                    new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                     new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                      new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                       new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                        new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                },
                         new SongsDeleted()
                {
                    Artist = "Zivert",
                    Filename = "zivert_-_life.mpe",
                    File_MD5 = "edh54hhehjh54wyhrtjr54",
                    File_MD5_Gained = "54hr4h4t54yw54yh",
                    Genre = "Pop",
                    Name = "BRussian Hits 2019", // playlist name
                    Title = "Life",
                    Uploaded = 1,
                    Sgid = 56,
                    Plid = 845
                }
            };
        }

        public async Task<List<Station>> GetFakeDataAsync()
        {
            await Task.Delay(5000);

            return new List<Station>()
            {
                new Station() { Name = "st.01", Blocked = 1, Licence = DateTime.Now, Uid=32, Soft_Installed = 0, Sid = 55, Autosync = 1, Hardware_ID = "00:e0:7d:02:b2:0a", Synchronized = 1, Type = 2 },
                new Station() { Name = "st.02" },
                new Station() { Name = "st.03" },
                new Station() { Name = "st.04" },
                new Station() { Name = "st.05" },
                new Station() { Name = "st.06" },
                new Station() { Name = "st.07" },
                new Station() { Name = "st.08" },
                new Station() { Name = "st.09" },
                new Station() { Name = "st.10" },
                new Station() { Name = "st.11" },
            };
        }
        #endregion
    }
}
