using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using Autofac.Integration.Web;
using FortuneCommon;
using FortuneCookieDatabase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Pivotal.Discovery.Client;
using Steeltoe.Extensions.Configuration;


namespace FortunesLegacyService
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        private static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        void Application_Start(object sender, EventArgs e)
        {

            ServerConfig.RegisterConfig("development", (env, configBuilder) => 
                configBuilder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddCloudFoundry()
                .AddEnvironmentVariables());

            var builder = new ContainerBuilder();
            builder.AddDbContext<FortuneCookieDbContext>(ServerConfig.Configuration);
            builder.AddDiscoveryService();
            builder.RegisterType<FortuneServiceWCF>();
            var container = builder.Build();
            _containerProvider = new ContainerProvider(container);
            AutofacHostFactory.Container = container;

        }
    }
}