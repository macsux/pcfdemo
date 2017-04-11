using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Web;
using FortuneCommon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Pivotal.Discovery.Client;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.Extensions.Configuration;

namespace FortunesFormsUI
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {

        private static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider => _containerProvider;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            ServerConfig.RegisterConfig("development", (env, configBuilder) =>
                configBuilder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddCloudFoundry()
                .AddEnvironmentVariables());

            
            var builder = new ContainerBuilder();
            builder.AddDiscoveryService();
            builder.RegisterType<LegacyCookieClient>().Named<ICookieService>("asmx");
            builder.RegisterType<WcfCookieClient>().Named<ICookieService>("wcf");
            builder.RegisterType<LocalCookieService>().Named<ICookieService>("local");
            
            builder.Register(c =>
            {
                var config = c.Resolve<IOptions<FortunesConfiguration>>();
                Func<string, ICookieService> clientFactory = serviceType => c.ResolveNamed<ICookieService>(config.Value.ClientType);
                return clientFactory;
            });
            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
    public static class sXTensions
    {


    }

    public class FortunesConfiguration
    {
        public string ClientType { get; set; }
    }
//    public enum CookieServiceType
//    {
//        Local,
//        Asmx,
//        Wcf,
//        Rest
//    }
}