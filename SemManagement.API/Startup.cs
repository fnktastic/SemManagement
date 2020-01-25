using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SemManagement.API.Scheduler;
using SemManagement.API.Scheduler.Jobs;
using SemManagement.MonitoringContext.DataAccess;
using SemManagement.MonitoringContext.Repository;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<SemDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("SemDBConnection")));
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStationRepository, StationRepository>();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            services.AddDbContext<MonitoringDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MonitoringDBConnection")));
            services.AddTransient<IMonitoringRepositry, MonitoringRepositry>();

            services.AddTransient<HelloJob>();
            var container = services.BuildServiceProvider();
            // Create an instance of the job factory
            var jobFactory = new JobFactory(container);
            // Create a Quartz.NET scheduler
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            // Tell the scheduler to use the custom job factory
            scheduler.JobFactory = jobFactory;
            services.AddSingleton<IScheduler>(scheduler);
            scheduler.Start().Wait();

            var monitoringScheduler = new MonitoringScheduler(scheduler);
            services.AddSingleton<MonitoringScheduler>(monitoringScheduler);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, MonitoringDbContext monitoringDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            DbInitializer.Initialize(monitoringDbContext);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
