using dot_net_core_in_memory_caching.Interfaces;
using dot_net_core_in_memory_caching.Services;
using Microsoft.OpenApi.Models;

namespace dot_net_core_in_memory_caching
{
  public class Startup
  {

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public string appName { get; set; }
    public string appVersion { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      appName = Configuration.GetValue<string>("Application:Name").ToString();
      appVersion = Configuration.GetValue<string>("Application:Version").ToString();

      services.AddMemoryCache();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = appName , Version = appVersion });
      });
      services.AddResponseCaching();
      services.AddSingleton<ITrailService, GetTrailService>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} {appVersion}"));
      }
      else
      {
        app.UseHsts();
      }

      app.UseRouting();
      app.UseAuthorization();
      app.UseResponseCaching();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
