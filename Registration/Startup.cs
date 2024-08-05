using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using UserReg.Models;
using UserReg.Data;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                Configuration.GetConnectionString("Myconnection"),
                new MySqlServerVersion(new Version(8, 0, 21))
            )
        );

        // Add Authentication and Authorization services
        services.AddAuthentication("YourAuthenticationScheme")
            .AddCookie("YourAuthenticationScheme", options =>
            {
                options.LoginPath = "/Account/Login";
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("YourPolicyName", policy =>
                policy.RequireAuthenticatedUser());
        });

        services.AddControllersWithViews();
    }



    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Registration}/{action=create}/{id?}");
        });
    }
}
