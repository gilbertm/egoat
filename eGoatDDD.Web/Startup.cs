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
using Microsoft.AspNetCore.Identity.UI.Services;
using eGoatDDD.Web.Infrastructure;
using Microsoft.AspNetCore.Identity.UI;
using SixLabors.ImageSharp.Web.DependencyInjection;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.IO;

namespace eGoatDDD.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;

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

            // Add the default service and options.
            services.AddImageSharp();

            // https://medium.com/volosoft/convert-html-and-export-to-pdf-using-dinktopdf-on-asp-net-boilerplate-e2354676b357
           var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
            var wkHtmlToPdfPath = Path.Combine(_webHostEnvironment.ContentRootPath, $"wkhtmltox\\v0.12.4\\{architectureFolder}\\libwkhtmltox");
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

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
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Stores.MaxLengthForKeys = 128;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<eGoatDDDDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            #endregion

            #region ++g++ smtp
            services.AddTransient<IEmailSender, EmailSender>(i =>
               new EmailSender(
                   Configuration["EmailSender:Host"],
                   Configuration.GetValue<int>("EmailSender:Port"),
                   Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                   Configuration["EmailSender:UserName"],
                   Configuration["EmailSender:Password"],
                   Configuration["EmailSender:UserEmail"]
               )
           );
            #endregion

            #region ++g++ configure all Claims Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrators", policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
                options.AddPolicy("PowerUsers", policy => policy.RequireClaim(ClaimTypes.Role, "PowerUser"));
                options.AddPolicy("Supervisors", policy => policy.RequireClaim(ClaimTypes.Role, "Supervisor"));
                options.AddPolicy("Attendants", policy => policy.RequireClaim(ClaimTypes.Role, "Attendant"));

                options.AddPolicy("CanEdits", policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Administrator", "Poweruser", "Supervisor" }));
                options.AddPolicy("CanDisposes", policy => policy.RequireClaim(ClaimTypes.Role, new string[] { "Administrator", "Poweruser", "Supervisor" }));
            });
            #endregion

            services.AddTransient<ImageWriter.Interface.IImageWriter,
                                  ImageWriter.Classes.ImageWriter>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
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
