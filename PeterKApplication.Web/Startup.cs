using System;
using System.Text;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PeterKApplication.Shared.Data;
using PeterKApplication.Web.Services;
using PeterKApplication.Shared.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace PeterKApplication.Web
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureBoth(services);
        }

        private void ConfigureBoth(IServiceCollection services)
        {
            services.AddDefaultIdentity<AppUser>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRole.Owner, builder => builder.RequireRole(UserRole.Owner));
                options.AddPolicy(UserRole.Administrator, builder => builder.RequireRole(UserRole.Administrator));
                options.AddPolicy(UserRole.Agent, builder => builder.RequireRole(UserRole.Administrator));
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ISyncService, SyncService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IVisaService, VisaService>();
            services.AddScoped<IMPesaService, MPesaService>();
            services.AddScoped<IKnowledgeBaseService, KnowledgeBaseService>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvcCore().AddApiExplorer();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "API", Version = "v1"}); });
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            ConfigureBoth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext appDbContext,
            UserManager<AppUser> userManager)
        {
            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseEndpointRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"); });

            try
            {
                appDbContext.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed migrating database:" + e.Message);
            }

            AppDbInitializer.SeedAdmins(userManager);
            AppDbInitializer.SeedAgents(userManager);
        }
    }
}