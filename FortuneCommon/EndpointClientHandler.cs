using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using Pivotal.Discovery.Client;

namespace FortuneCommon
{
    public class EndpointClientHandler : DiscoveryHttpClientHandler, IDiscoveryClientAddressResolver
    {
        public EndpointClientHandler(IDiscoveryClient client) : base(client)
        {
        }

        public EndpointAddress GetEndpointAddress(string endpointName)
        {
            var clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");
            var endpointAddress = clientSection.Endpoints.Cast<ChannelEndpointElement>()
                .Where(x => x.Name == endpointName)
                .Select(x => new EndpointAddress(LookupService(x.Address)))
                .FirstOrDefault();
            return endpointAddress;
        }

        Uri IDiscoveryClientAddressResolver.LookupService(Uri uri)
        {
            return LookupService(uri);
        }
        Uri IDiscoveryClientAddressResolver.LookupService(string uri)
        {
            return LookupService(new Uri(uri));
        }
    }

    public interface IDiscoveryClientAddressResolver
    {
        EndpointAddress GetEndpointAddress(string endpointName);
        Uri LookupService(Uri uri);
        Uri LookupService(string uri);
    }
}
