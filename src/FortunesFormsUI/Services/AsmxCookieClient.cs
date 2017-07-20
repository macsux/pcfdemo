using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FortuneCommon;

namespace FortunesFormsUI
{
    public class AsmxCookieClient : ICookieService
    {
        private readonly IDiscoveryClientAddressResolver _dicoveryAddressResolver;

        public AsmxCookieClient(IDiscoveryClientAddressResolver dicoveryAddressResolver)
        {
            _dicoveryAddressResolver = dicoveryAddressResolver;
        }

        public string GetCookie()
        {
            var client = new FortuneServiceLegacy.FortuneServiceLegacy();
            var uri = "http://FortunesLegacyService" + new Uri(client.Url).AbsolutePath;
            client.Url = _dicoveryAddressResolver.LookupService(uri).ToString();
            return client.GetCookie();
        }

#pragma warning disable 1998
        public async Task<string> GetCookieAsync()
#pragma warning restore 1998
        {
            return GetCookie();
        }
    }
}