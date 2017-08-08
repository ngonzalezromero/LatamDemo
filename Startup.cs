using System;
using Latam.Infraestructure;
using Latam.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Latam
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EFContext>(options => options.UseNpgsql(Configuration["AppSettings:DBConnection:postgre"]));
            services.AddScoped<EFRepository>();
            services.AddScoped<MemoryRepository>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddMvc()
           .AddJsonOptions(opt =>
            {
                var resolver = opt.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    var res = resolver as DefaultContractResolver;
                    res.NamingStrategy = null;
                }
            });
        }

        public void Configure(IServiceProvider service, IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
            app.Use(async (context, next) =>
              {
                  var path = context.Request.Path.Value;

                  if (path == "/")
                  {
                      context.Response.Redirect("Home/List");
                  }
                  else if (path == "/Home")
                  {
                      context.Response.Redirect("Home/List");
                  }
                  else if (path == "Home/" || path == "/Home/")
                  {
                      context.Response.Redirect("List");
                  }
                  else
                  {
                      await next();
                  }
              }
           );
            InitializeDatabase(service);
        }
        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<EFContext>().Database.Migrate();
            }
        }
    }
}