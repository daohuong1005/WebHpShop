using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebHpShop.Common.Authentication;
using WebHpShop.Reponsitory;
using WebHpShop.Reponsitory.Interfaces;
using WebHpShop.Service;
using WebHpShop.Service.Interface;
using WebHpShop.Service.Interfaces;
using WebHpShop.Service.Services;
using WebHpShop.Service.ViewModel;
using WebStore.Domain;
using WebStore.Reponsitory;

namespace WebHpShop.Portals
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
            services.AddDbContext<WebHpShopDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("MyConnection"));
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(20000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AuthencationSetting>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AuthencationSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = long.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // injection
            ConfigureCoreAndRepositoryService(services);

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews();
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

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        private void ConfigureCoreAndRepositoryService(IServiceCollection services)
        {
            services.AddScoped(typeof(IReponsitory<>), typeof(BaseReponsitory<>));
            services.AddScoped(typeof(IServices<>), typeof(BaseService<>));


            services.AddScoped<IUserReponsitory, UserReponsitory>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ISupplierReponsitory, SupplierReponsiory>();
            services.AddScoped<ISupplierService, SupplierService>();

            services.AddScoped<IManufacturerReponsitory, ManufacturerReponsitory>();
            services.AddScoped<IManufacturerService, ManufacturerService>();

            services.AddScoped<ICategoryReponsitory, CategoryReponsitory>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductReponsitory, ProductReponsitory>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IDiscountReponsitory, DiscountReponsitory>();
            services.AddScoped<IDiscountService, DiscountService>();

        }
    }
}
