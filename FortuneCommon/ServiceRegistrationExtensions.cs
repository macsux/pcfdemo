using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Pivotal.Discovery.Client;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.Services;

namespace FortuneCommon
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddDiscoveryService(this ContainerBuilder services)
        {
            EurekaServiceInfo info = ServerConfig.Configuration.GetSingletonServiceInfo<EurekaServiceInfo>();
            DiscoveryOptions configOptions = new DiscoveryOptions(ServerConfig.Configuration)
            {
                ClientType = DiscoveryClientType.EUREKA
            };

            DiscoveryClientFactory factory = new DiscoveryClientFactory(info, configOptions);
            var discoveryClient = (IDiscoveryClient)factory.CreateClient();
            services.Register(c => discoveryClient).SingleInstance().AutoActivate();
            services.Register(c => new EndpointClientHandler(discoveryClient)).AsImplementedInterfaces().SingleInstance();

        }
    }

}
