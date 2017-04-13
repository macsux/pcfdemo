using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FortuneCommon;

namespace FortunesFormsUI
{
    public class LegacyCookieClient : ICookieService
    {
        private readonly IDiscoveryClientAddressResolver _dicoveryAddressResolver;

        public LegacyCookieClient(IDiscoveryClientAddressResolver dicoveryAddressResolver)
        {
            _dicoveryAddressResolver = dicoveryAddressResolver;
        }

        public async Task<string> GetCookie()
        {
            var client = new FortuneServiceLegacy.FortuneServiceLegacy();
            client.Url = _dicoveryAddressResolver.LookupService(client.Url).ToString();
            return client.GetCookie();
        }
    }
}