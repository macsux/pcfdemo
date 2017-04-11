using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FortuneCommon;

namespace FortunesFormsUI
{
    public partial class _Default : Page
    {
        private ICookieService _cookieService;

        public IDiscoveryClientAddressResolver DiscoveryAddressResolver { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
//            _cookieService = new LocalCookieService();
//            _cookieService = new LegacyCookieClient(Global.DiscoveryAddressResolver);
            _cookieService = new WcfCookieClient(DiscoveryAddressResolver);
        }

        protected void btnGetCookie_OnClick(object sender, EventArgs e)
        {
            this.lblCookie.Text = _cookieService.GetCookie();
            pnlCookieResult.Visible = true;
            
        }
    }



    public class LegacyCookieClient : ICookieService
    {
        private readonly IDiscoveryClientAddressResolver _dicoveryAddressResolver;

        public LegacyCookieClient(IDiscoveryClientAddressResolver dicoveryAddressResolver)
        {
            _dicoveryAddressResolver = dicoveryAddressResolver;
        }

        public string GetCookie()
        {
            var client = new FortuneServiceLegacy.FortuneServiceLegacy();
            client.Url = _dicoveryAddressResolver.LookupService(client.Url).ToString();
            return client.GetCookie();
        }
    }

    public class WcfCookieClient : ICookieService
    {
        private IDiscoveryClientAddressResolver _dicoveryAddressResolver;

        public WcfCookieClient(IDiscoveryClientAddressResolver dicoveryAddressResolver)
        {
            _dicoveryAddressResolver = dicoveryAddressResolver;
        }

        public string GetCookie()
        {
            
            var channelFactory = new ChannelFactory<ICookieService>("FortuneServiceWcf", _dicoveryAddressResolver.GetEndpointAddress("FortuneServiceWcf"));
            return channelFactory.CreateChannel().GetCookie();
        }
    }
}