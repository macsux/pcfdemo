using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Web;
using FortuneCommon;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pivotal.Discovery.Client;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.Extensions.Configuration;
using DiscoveryApplicationBuilderExtensions = Steeltoe.Discovery.Client.DiscoveryApplicationBuilderExtensions;

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
//                    .AddConfigServer(env)
                    .AddEnvironmentVariables());


            var services = new ServiceCollection();
            services.AddDiscoveryClient(ServerConfig.Configuration);
            services.AddOptions();
            services.Configure<FortunesConfiguration>(ServerConfig.Configuration.GetSection("fortunes"));

            var builder = new ContainerBuilder();
            builder.Populate(services);
            // used for wcf integration
            builder.RegisterType<EndpointClientHandler>().AsImplementedInterfaces().SingleInstance();
//            builder.AddDiscoveryService();

            builder.RegisterType<LegacyCookieClient>().Named<ICookieService>("asmx");
            builder.RegisterType<WcfCookieClient>().Named<ICookieService>("wcf");
            builder.RegisterType<LocalCookieService>().Named<ICookieService>("local");
            builder.RegisterType<RestCookieClient>().Named<ICookieService>("rest");

            builder.Register(c =>
            {
                var localContext = c.Resolve<IComponentContext>();
                var config = c.Resolve<IOptionsSnapshot<FortunesConfiguration>>();
                Func<ICookieService> clientFactory = () => localContext.ResolveNamed<ICookieService>(config.Value.ClientType);
                return clientFactory;
            });
            builder.Register(c =>
            {
                var localContext = c;
                return localContext.Resolve<Func<ICookieService>>()();
            });
            var container = builder.Build();
            // ensure that discovery client component starts up
            container.Resolve<IDiscoveryClient>();
//            var factory = container.Resolve<Func<ICookieService>>();
//            var client = factory();
            // needed for WinForms injection
            _containerProvider = new ContainerProvider(container);
            
        }
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