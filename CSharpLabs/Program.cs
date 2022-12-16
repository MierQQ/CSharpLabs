using System.Text;
using CSharpLabs.MarriageProblem.Contender.ContenderBuilder;
using CSharpLabs.MarriageProblem.ContenderGenerator;
using CSharpLabs.MarriageProblem.DataBase;
using CSharpLabs.MarriageProblem.Freind;
using CSharpLabs.MarriageProblem.Hall;
using CSharpLabs.MarriageProblem.Princess;
using CSharpLabs.MarriageProblem.Web;
using CSharpLabs.MarriageProblem.Writer;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace CSharpLabs;

class Program
{
    protected Program(){}
    public static void Main(string[] args)
    {
        using (var db = new ApplicationContext())
        {
            //CreateHostBuilder(args).Build().Start();
            /*
            for (int i = 0; i < 100; ++i) 
                CreateDbHostBuilder(args, db).Build().Start();
            */
            var webHost = CreateWebBuilder(args).Build();
            webHost.StartAsync();
            CreateHostHttpBuilder(args).Build().Start();
            webHost.StopAsync().Wait();
        }
    }

    private static IHostBuilder CreateHostHttpBuilder(string[] args)
    {
        var contenderNumber = int.Parse(ConfigurationManager.AppSettings["contenderNumber"] ?? string.Empty);
        var threshold = int.Parse(ConfigurationManager.AppSettings["threshold"] ?? string.Empty);
        var attempt = int.Parse(ConfigurationManager.AppSettings["attempt"] ?? string.Empty);
        var session = int.Parse(ConfigurationManager.AppSettings["session"] ?? string.Empty);
        var url = ConfigurationManager.AppSettings["url"] ?? string.Empty;
        return Host.CreateDefaultBuilder(args).ConfigureServices(
            services => services
                .AddHostedService(x=>new MarriageProblem.MarriageProblemWeb(
                    contenderNumber,
                    x.GetRequiredService<IPrincessWeb>(),
                    attempt,
                    url,
                    session))
                .AddScoped<IPrincessWeb>(x => new PrincessWeb(contenderNumber, threshold, url))
        );
    }
    
    private static IHostBuilder CreateWebBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
        {
            builder.UseStartup<Startup>();
        });
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var contenderNumber = int.Parse(ConfigurationManager.AppSettings["contenderNumber"] ?? string.Empty);
        var threshold = int.Parse(ConfigurationManager.AppSettings["threshold"] ?? string.Empty);
        return Host.CreateDefaultBuilder(args).ConfigureServices(
            services => services
                .AddHostedService(x=>new MarriageProblem.MarriageProblem(
                    contenderNumber,
                    x.GetRequiredService<IPrincess>(),
                    x.GetRequiredService<IHall>(), x.GetRequiredService<IWriter>()))
                .AddScoped<IWriter>(_ => new FileWriter(new StreamWriter("out.txt", false, Encoding.UTF8)))
                .AddScoped<IPrincess>(x=> new Princess(
                    x.GetRequiredService<IFreind>(),
                    contenderNumber,
                    threshold))
                .AddScoped<IFreind, Freind>()
                .AddScoped<IHall>(x => new Hall(
                    contenderNumber,
                    x.GetRequiredService<IContenderGenerator>()))
                .AddScoped<IContenderGenerator, DefaultContenderGenerator>()
                .AddScoped<IContenderBuilder, ContenderBuilder>()
        );
    }
    
    private static IHostBuilder CreateDbHostBuilder(string[] args, ApplicationContext applicationContext)
    {
        var contenderNumber = int.Parse(ConfigurationManager.AppSettings["contenderNumber"] ?? string.Empty);
        var threshold = int.Parse(ConfigurationManager.AppSettings["threshold"] ?? string.Empty);
        return Host.CreateDefaultBuilder(args).ConfigureServices(
            services => services
                .AddHostedService(x=>new MarriageProblem.MarriageProblem(
                    contenderNumber,
                    x.GetRequiredService<IPrincess>(),
                    x.GetRequiredService<IHall>(), x.GetRequiredService<IWriter>()))
                .AddScoped<IWriter>(_ => new DbWriter(applicationContext))
                .AddScoped<IPrincess>(x=> new Princess(
                    x.GetRequiredService<IFreind>(),
                    contenderNumber,
                    threshold))
                .AddScoped<IFreind, Freind>()
                .AddScoped<IHall>(x => new Hall(
                    contenderNumber,
                    x.GetRequiredService<IContenderGenerator>()))
                .AddScoped<IContenderGenerator>(x => new DbContenderGenerator(x.GetRequiredService<IContenderBuilder>(), applicationContext))
                .AddScoped<IContenderBuilder, ContenderBuilder>()
        );
    }
    
    private static IHostBuilder CreateDbAttemptHostBuilder(string[] args, ApplicationContext applicationContext, int attemptNumber)
    {
        var contenderNumber = int.Parse(ConfigurationManager.AppSettings["contenderNumber"] ?? string.Empty);
        var threshold = int.Parse(ConfigurationManager.AppSettings["threshold"] ?? string.Empty);
        return Host.CreateDefaultBuilder(args).ConfigureServices(
            services => services
                .AddHostedService(x=>new MarriageProblem.MarriageProblem(
                    contenderNumber,
                    x.GetRequiredService<IPrincess>(),
                    x.GetRequiredService<IHall>(), x.GetRequiredService<IWriter>()))
                .AddScoped<IWriter>(_ => new FileWriter(new StreamWriter("out.txt", false, Encoding.UTF8)))
                .AddScoped<IPrincess>(x=> new Princess(
                    x.GetRequiredService<IFreind>(),
                    contenderNumber,
                    threshold))
                .AddScoped<IFreind, Freind>()
                .AddScoped<IHall>(x => new Hall(
                    contenderNumber,
                    x.GetRequiredService<IContenderGenerator>()))
                .AddScoped<IContenderGenerator>(x => new DbContenderGeneratorByAttemptNumber(x.GetRequiredService<IContenderBuilder>(), applicationContext, attemptNumber))
                .AddScoped<IContenderBuilder, ContenderBuilder>()
        );
    }
}
