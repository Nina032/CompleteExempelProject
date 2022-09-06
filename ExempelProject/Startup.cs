using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExempelProject.Data;
using ExempelProject.Data.Entities;
using ExempelProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ExempelProject
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentity<StoreUser, IdentityRole>(cfg =>
      {
        cfg.User.RequireUniqueEmail = true;
      })
        .AddEntityFrameworkStores<ExempelContext>();

      services.AddAuthentication()
        .AddCookie()
        .AddJwtBearer(cfg =>
        {
          cfg.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidIssuer = _config["Tokens:Issuer"],
            ValidAudience = _config["Tokens:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
          };
        });

      services.AddDbContext<ExempelContext>();
      services.AddTransient<IMailService, NullMailService>();

      services.AddTransient<ExempelSeeder>();

      services.AddAutoMapper(Assembly.GetExecutingAssembly());

      services.AddScoped<IExempelRepository, ExempelRepository>();

      services.AddControllersWithViews()
        .AddRazorRuntimeCompilation()
        .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

      services.AddRazorPages();
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
        // Add Error Page
        app.UseExceptionHandler("/error");
      }

      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(cfg =>
      {
        cfg.MapControllerRoute("Fallback",
          "{controller}/{action}/{id?}",
          new { controller = "App", action = "Index" });

        cfg.MapRazorPages();
      });
    }
  }
}
