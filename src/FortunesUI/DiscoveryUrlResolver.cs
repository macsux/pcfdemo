using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pivotal.Discovery.Client;

namespace FortunesUI
{
    public class DiscoveryUrlResolver : DiscoveryHttpClientHandler
    {
        public DiscoveryUrlResolver(IDiscoveryClient client) : base(client)
        {
        }

        public Uri LookupService(string uri)
        {
            return LookupService(new Uri(uri));
        }
    }
}

