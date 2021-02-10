using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionApp.Factory;
using AuctionApp.Hubs;
using AuctionApp.Models.Database;
using AuctionApp.Models.Initializers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuctionApp
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
            services.AddDbContext<AuctionAppContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("AuctionAppDB"))
            );
            services.AddIdentity<User,IdentityRole>(
                options => {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric=false;
                }
            ).AddEntityFrameworkStores<AuctionAppContext>().AddDefaultTokenProviders();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddAutoMapper(typeof(Startup));
            
            services.ConfigureApplicationCookie(
                options => {
                    options.LoginPath = "/User/LogIn";

                    //Ako nemas pravo pristupa idi na ovu stracnicu
                    options.AccessDeniedPath = "/Error";
                }
            );

            //ovde namestam da se koristi i claim factory i kad god se koristi dobicemo parove kljuc vrednost vezane za userName
            services.AddScoped<IUserClaimsPrincipalFactory<User>,ClaimFactory>();

            //Da bi sve bilo sinhronizovano tj metode koje se pozivaju za sve odrejdene klijente
            services.AddSignalR();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> UserManager)
        {
            if (env.IsDevelopment())
            {
                //UserInitializer.initialize(UserManager);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // //proba s ovim
            // app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {   
                endpoints.MapHub<AuctionHub>("/auctionUpdate");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
