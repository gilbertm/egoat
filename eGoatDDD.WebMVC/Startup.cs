using eGoatDDD.Application.Products.Commands;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Claims;

namespace eGoatDDD.WebMVC
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

            // Add framework services.
            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(CreateProductCommandHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<eGoatDDDDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("eGoatDDDDatabase"), b => b.MigrationsAssembly("eGoatDDD.Persistence")));

            // Customizing Identity
            // Reference: https://blogs.msdn.microsoft.com/webdev/2018/03/02/aspnetcore-2-1-identity-ui
            #region ++g++ allows identity overrides
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<eGoatDDDDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            #endregion

            #region ++g++ configure all Claims Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrators", policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
                options.AddPolicy("Lessees", policy => policy.RequireClaim(ClaimTypes.Role, "Lessee"));
                options.AddPolicy("Lenders", policy => policy.RequireClaim(ClaimTypes.Role, "Lender"));
            });
            #endregion

            services.AddMvc();
            // .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region ++g++ seed roles
            eGoatDDDInitializerRoles.SeedRoles(app.ApplicationServices).Wait();
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
