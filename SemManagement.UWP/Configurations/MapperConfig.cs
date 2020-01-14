using AutoMapper;
using SemManagement.Local.Storage.Model;
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

            cfg.CreateMap<RuleDto, Rule>();
        })
        {

        }
    }
}
