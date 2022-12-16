using CSharpLabs.MarriageProblem.Web.Controllers;

namespace CSharpLabs.MarriageProblem.Web;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddControllers().AddControllersAsServices();
        services.AddSingleton<FreindController>();
        services.AddSingleton<HallController>();
        
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting ();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}