using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FortuneCommon;

namespace FortunesFormsUI
{
    public partial class Cookies : System.Web.UI.Page
    {
        public ICookieService CookieService { get; set; }

        public IDiscoveryClientAddressResolver DiscoveryAddressResolver { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //            _cookieService = new LocalCookieService();
            //            _cookieService = new LegacyCookieClient(Global.DiscoveryAddressResolver);
            //            _cookieService = new WcfCookieClient(DiscoveryAddressResolver);
        }

        protected async void btnGetCookie_OnClick(object sender, EventArgs e)
        {
            this.lblCookie.Text = await CookieService.GetCookie();
            this.lblCookieProvider.Text = CookieService.GetType().ToString();
            pnlCookieResult.Visible = true;


        }

    }
}