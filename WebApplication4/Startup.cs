using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;
using WebApplication4.Models;

namespace WebApplication4
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

      services.AddDbContext<AplicationDbContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConect")));

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
    {

      logger.LogInformation("In Development environment");

      if (env.IsDevelopment())
      {
        logger.LogInformation("In Development environment");
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }


      //if (env.IsDevelopment())
      //{
      //  app.UseDeveloperExceptionPage();
      //}
      //else
      //{
      //
      //  app.UseExceptionHandler("/Home/Error");
      //  //app.UseExceptionHandler("/Error/500");
      //
      //}

      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });

      // Configuramos Rotativa indicándole el Path RELATIVO donde se
      // encuentran los archivos de la herramienta wkhtmltopdf.
      RotativaConfiguration.Setup(env, ".\\rotativa\\");

    }
  }
}
