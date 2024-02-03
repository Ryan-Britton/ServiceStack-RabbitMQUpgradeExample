using Microsoft.AspNetCore.Hosting;
using ServiceStack;
using ServiceStack.Script;
using ServiceStack.Messaging;
using ExampleApp.ServiceModel;

[assembly: HostingStartup(typeof(ExampleApp.FeatureMq))]

namespace ExampleApp
{
    public class FeatureMq : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) => builder
            .ConfigureAppHost(afterPluginsLoaded:appHost => {
                var mqServer = appHost.Resolve<IMessageService>();
                mqServer.RegisterHandler<TestMq>(appHost.ExecuteMessage);
                appHost.Plugins.Add(new SharpPagesFeature());
                appHost.AssertPlugin<SharpPagesFeature>().ScriptMethods.Add(new MqScripts(mqServer));
                View.NavItems.Add(new NavItem {
                    Label = "Messaging",
                    Href = "/messaging",
                });
            });
    }

    public class MqScripts : ScriptMethods
    {
        IMessageService mq;
        public MqScripts(IMessageService mqService) => this.mq = mqService;

        public IMessageService mqService() => mq;
        public string mqStatsDescription(IMessageService mqService) => mqService.GetStatsDescription();
    }
}
