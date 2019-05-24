using AutoMapper;
using eGoatDDD.Application.Goats.Commands;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using eGoatDDD.Persistence.Repository;
using eGoatDDD.Persistence.Service.User;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;


namespace eGoatDDD.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // services.AddTransient<IEmailSender, EmailSender>();

            // Add framework services.
            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            // services.AddMediatR();
            services.AddMediatR(typeof(CreateGoatCommandHandler).GetTypeInfo().Assembly);

            services.AddHttpContextAccessor();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<eGoatDDDDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("eGoatDDDDatabase"), b => b.MigrationsAssembly("eGoatDDD.Persistence")));

            // Customizing Identity
            // Reference: https://blogs.msdn.microsoft.com/webdev/2018/03/02/aspnetcore-2-1-identity-ui
            #region ++g++ allows identity overrides
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.Stores.MaxLengthForKeys = 128)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<eGoatDDDDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            #endregion
            
            #region ++g++ configure all Claims Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrators", policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
                options.AddPolicy("PowerUsers", policy => policy.RequireClaim(ClaimTypes.Role, "PowerUser"));
                options.AddPolicy("Supervisors", policy => policy.RequireClaim(ClaimTypes.Role, "Supervisor"));
                options.AddPolicy("Attendants", policy => policy.RequireClaim(ClaimTypes.Role, "Attendant"));

                options.AddPolicy("CanEdits", policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Administrator", "Poweruser", "Supervisor" }));
                options.AddPolicy("CanDisposes", policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Administrator", "Poweruser" }));
            });
            #endregion

            services.AddTransient<ImageWriter.Interface.IImageWriter,
                                  ImageWriter.Classes.ImageWriter>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region ++g++ seed roles
            eGoatDDDInitializerRoles.SeedRoles(app.ApplicationServices).Wait();

            eGoatDDDInitializerAdminUser.SeedAdminUserAsync(userManager).Wait();
            #endregion

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Goat}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
