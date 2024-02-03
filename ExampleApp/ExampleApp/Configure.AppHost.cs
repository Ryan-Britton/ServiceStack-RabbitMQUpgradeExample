using System.Reflection;
using Funq;
using ServiceStack;
using ExampleApp.ServiceInterface;

[assembly: HostingStartup(typeof(ExampleApp.AppHost))]

namespace ExampleApp;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("ExampleApp", typeof(MyServices).Assembly, typeof(MqServices).Assembly) {}

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });
    }
}
