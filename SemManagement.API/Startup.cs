using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<SemDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("SemDBConnection")));
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStationRepository, StationRepository>();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            services.AddDbContext<MonitoringDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MonitoringDBConnection")));
            services.AddTransient<IMonitoringRepositry, MonitoringRepositry>();
            services.AddTransient<IMonitoringService, MonitoringService>();

            services.AddQuartz();
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
