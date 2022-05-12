using AutoMapper;
using LibraryManager.API.Mapper;
using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Managers;
using LibraryManager.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LibraryManager.API
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
            services.AddControllersWithViews();

            services.AddDbContext<LibraryContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("LibraryDatabase"));
                options.UseLazyLoadingProxies();
            });

            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                 .AddEntityFrameworkStores<LibraryContext>()
                 .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Forbidden/";
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/auth/login";
            });

            services.AddAuthorization();

            ConfigureMapper(services);
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IBookManager, BookManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Book}/{action=List}/{id?}");
            });
        }

        private static void ConfigureMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new MapperProfile()));

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
