﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.Services;
using SemManagement.SemContext;
using SemManagement.SemContext.Repository;

namespace SemManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(x => x.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<SemDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("SemDBConnection")));
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStationRepository, StationRepository>();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            services.AddDbContext<MonitoringDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MonitoringDBConnection")));
            services.AddTransient<IMonitoringRepositry, MonitoringRepositry>();
            services.AddTransient<IMonitoringService, MonitoringService>();
            services.AddTransient<ISchedulerService, SchedulerService>();

            services.AddTransient<ILocalPlaylistRepository, LocalPlaylistRepository>();
            services.AddTransient<ILocalRulesRepository, LocalRulesRepository>();
            services.AddTransient<ILocalStationRepository, LocalStationRepository>();
            services.AddTransient<ILocalTagRepository, LocalTagRepository>();
            services.AddTransient<IRuleService, RuleService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ISnapshotEntryRepository, SnapshotEntryRepository>();
            services.AddTransient<IFeedRepository, FeedRepository>();

            services.AddQuartz();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, MonitoringDbContext monitoringDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //DbInitializer.Initialize(monitoringDbContext);

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
