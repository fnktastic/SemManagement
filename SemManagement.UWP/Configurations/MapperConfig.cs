using AutoMapper;
using SemManagement.LocalContext.Model;
using SemManagement.UWP.Model.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Configurations
{
    public class MyMapper : Mapper
    {
        public MyMapper(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {

        }
    }

    public class MyConfig : MapperConfiguration
    {
        public MyConfig() : base(cfg =>
        {
            cfg.CreateMap<Rule, RuleDto>();
            cfg.CreateMap<Playlist, Model.Playlist>();
            cfg.CreateMap<Station, Model.Station>();
            cfg.CreateMap<Station, StationDto>();
            cfg.CreateMap<Model.Station, StationDto>();
            cfg.CreateMap<Playlist, PlaylistDto>();
            cfg.CreateMap<Model.Playlist, PlaylistDto>();
            cfg.CreateMap<Tag, TagDto>();
            cfg.CreateMap<Monitoring, MonitoringDto>();

            cfg.CreateMap<RuleDto, Rule>();
            cfg.CreateMap<Model.Playlist, Playlist>();
            cfg.CreateMap<Model.Station, Station>();
            cfg.CreateMap<StationDto, Station>();
            cfg.CreateMap<StationDto, Model.Station>();
            cfg.CreateMap<PlaylistDto, Playlist>();
            cfg.CreateMap<PlaylistDto, Model.Playlist>();
            cfg.CreateMap<TagDto, Tag>();
            cfg.CreateMap<MonitoringDto, Monitoring>();
        })
        {

        }
    }
}
