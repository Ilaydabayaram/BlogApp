using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlogApp.Data;
using BlogApp.Middlewares;
using BlogApp.Filters;
using BlogApp.Filter;

namespace Blog_Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();
            // Added filters with dependency injection into the container.
            builder.Services.AddScoped<ActionFilter>();

            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(30);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<RequestTimingMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always, //Always HTTPS request (security)
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}