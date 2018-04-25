using System;
using System.Threading.Tasks;
using BoardGames.Data;
using BoardGames.Models;
using BoardGames.Repositories;
using BoardGames.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGames
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // Autoryzacja??
        private async Task CreateRoles(IServiceProvider serviceProvider) {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames) {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist) {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var admin = new ApplicationUser { UserName = "admin@admins.com", Email = "admin@admins.com" };
            string password = "P@ssw0rd";
            var _user = await UserManager.FindByEmailAsync("admin@admins.com");

            if (_user == null) {
                var createPowerUser = await UserManager.CreateAsync(admin, password);
                if (createPowerUser.Succeeded) {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
        // ------------------


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPublishersRepository, PublishersRepository>();
            services.AddTransient<IGamesRepository, GamesRepository>();

            services.AddMvc();

            services.AddDbContext<BGContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BGContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /*
         public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        */
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider) {

            app.UseExceptionHandler(o => this.AddErrorHandling(o));
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateRoles(serviceProvider).Wait();
        }

        private IApplicationBuilder AddErrorHandling(IApplicationBuilder options) {
            options.Run(
                async context => {
                    context.Response.ContentType = "text/html";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    var ip = context.Connection.LocalIpAddress.ToString();
                    if (ex != null && ip.Contains("127.0.0.1")) {
                        var err = $"<h1>Your IP: {ip}</h1><br/><h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                        await context.Response.WriteAsync(err);
                    } else {
                        var err = $"<h1>Your IP: {ip}</h1><br/><h1>Error!</h1>";
                        await context.Response.WriteAsync(err);
                    }
                });

            return options;
        }
    }
}
