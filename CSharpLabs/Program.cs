using System.Text;
using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.Freind;
using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Princess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSharpLabs;

class Program
{
    protected Program()
    {
    }
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Start();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var contenderNumber = int.Parse(args[0]);
        var threshold = int.Parse(args[1]);
        return Host.CreateDefaultBuilder(args).ConfigureServices(
            services => services
                .AddHostedService(x=>new MarriageProblem.MarriageProblem(
                    contenderNumber,
                    x.GetRequiredService<IPrincess>(),
                    x.GetRequiredService<IHall>(), new StreamWriter("out.txt", false, Encoding.UTF8)))
                .AddScoped<IPrincess>(x=> new Princess(
                    x.GetRequiredService<IFreind>(),
                    contenderNumber,
                    threshold))
                .AddScoped<IFreind, Freind>()
                .AddScoped<IHall>(x => new Hall(
                    contenderNumber,
                    x.GetRequiredService<IContenderGenerator>()))
                .AddScoped<IContenderGenerator, DefaultContenderGenerator>()
        );
    }
}