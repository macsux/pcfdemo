using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using FortuneCommon;

namespace FortunesFormsUI
{
    public class WcfCookieClient : ICookieService
    {
        private IDiscoveryClientAddressResolver _dicoveryAddressResolver;

        public WcfCookieClient(IDiscoveryClientAddressResolver dicoveryAddressResolver)
        {
            _dicoveryAddressResolver = dicoveryAddressResolver;
        }

        public async Task<string> GetCookie()
        {

            var channelFactory = new ChannelFactory<ICookieService>("FortuneServiceWcf", _dicoveryAddressResolver.GetEndpointAddress("FortuneServiceWcf"));
            return await channelFactory.CreateChannel().GetCookie();
        }
    }
}