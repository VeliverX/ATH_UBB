using ATH_UBB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ATH_UBB.Service;
using IRepositoryService;
using RepositoryService;
using ATH_UBB.Profiles;
using FluentValidation;
using ATH_UBB.Validation;
using ATH_UBB.Models;
using ATH_UBB.Areas.Admin.Models;

namespace ATH_UBB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("_dbcontext"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();
            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
            builder.Services.AddScoped(typeof(IValidator<VehicleDetailViewModel>), typeof(VehicleDetailValidator));
            builder.Services.AddScoped(typeof(IValidator<RentalPointViewModel>), typeof(RentalPointValidator));
            builder.Services.AddAutoMapper(typeof(VehicleProfile), typeof(RentalPoiontProfile));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //    endpoints.MapControllerRoute(
            //      name: "areas",
            //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

            app.MapControllerRoute(
               name: "MyArea",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
           
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();


            using (var scope = app.Services.CreateScope())
            {

                var _userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var _roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

                var createAdminRole = _roleManager.CreateAsync(
                    new ApplicationRole()
                    {
                        Name = "Administrator"
                    });

                var createUserRole = _roleManager.CreateAsync(
                    new ApplicationRole()
                    {
                        Name = "User"
                    });


                var createResult = _userManager.CreateAsync(
                new ApplicationUser()
                {
                    UserName = "admin@ath.edu",
                    Email = "admin@ath.edu",
                    LockoutEnabled = false,
                    AccessFailedCount = 0,



                }, "Haslo123!").Result;

                var createUser = _userManager.CreateAsync(
                new ApplicationUser()
                {
                    UserName = "nikodemn@ath.edu",
                    Email = "nikodem@ath.edu",
                    LockoutEnabled = false,
                    AccessFailedCount = 0,



                }, "Haslo123!").Result;

                var createUser1 = _userManager.CreateAsync(
                new ApplicationUser()
                {
                    UserName = "nikodemn1@ath.edu",
                    Email = "nikodem1@ath.edu",
                    LockoutEnabled = false,
                    AccessFailedCount = 0,



                }, "Haslo123!").Result;

                var adminUser = _userManager.FindByNameAsync("admin@ath.edu").Result;
                var User = _userManager.FindByNameAsync("nikodem@ath.edu").Result;
                var User1 = _userManager.FindByNameAsync("nikodem1@ath.edu").Result;
                //var code = _userManager.GenerateEmailConfirmationTokenAsync(adminUser).Result;
                //var result = _userManager.ConfirmEmailAsync(adminUser, code).Result;
                _userManager.AddToRoleAsync(adminUser, "Administrator");
                _userManager.AddToRoleAsync(User, "User");
                _userManager.AddToRoleAsync(User1, "User");

            }
            app.Run();
        }
    }
}