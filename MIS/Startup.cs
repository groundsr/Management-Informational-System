using MSI.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MIS.Data;
using MIS.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MIS.DataAccess.Abstractions;
using MIS.DataAccess;
using MIS.BusinessLogic;
using MIS.Model;
using MSI.Model;
using MIS.BusinessLogic.Filtering;

namespace MIS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PoliceContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Police")));

            services.AddScoped<IPoliceSectionRepository, EFPoliceSectionRepository>();
            services.AddScoped<ICriminalRecordRepository, EFCriminalRecordRepository>();
            services.AddScoped<IMeetingRequestRepository, EFMeetingRequestRepository>();
            services.AddScoped<ICriminalRecordPolicemanRepository, EFCriminalRecordPolicemanRepository>();
            services.AddScoped<IMeetingRequestPolicemanRepository, EFMeetingRequestPolicemanRepository>();
            services.AddScoped<IPolicemanRepository, EFPolicemanRepository>();
            services.AddScoped<IMeetingPolicemanRepository, EFMeetingPolicemanRepository>();
            services.AddScoped<IMeetingRepository, EFMeetingRepository>();
            services.AddScoped<IDocumentRepository, EFDocumentRepository>();
           

            services.AddScoped<PoliceSectionService>();
            services.AddScoped<MeetingRequestService>();
            services.AddScoped<PolicemanService>();
            services.AddScoped<CriminalRecordService>();
            services.AddScoped<MeetingService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("IdentityConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IManageConnectedUsers, ManageConnectedUsers>();
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
