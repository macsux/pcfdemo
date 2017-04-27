using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this.lblCookieProvider.Text = CookieService.GetType().ToString();
        }

        protected async void btnGetCookie_OnClick(object sender, EventArgs e)
        {
            this.lblCookie.Text = await CookieService.GetCookieAsync();


        }

        protected void btnKill_OnClick(object sender, EventArgs e)
        {
            try
            {
                throw new DivideByZeroException();
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception);
                Environment.Exit(-1);
                
            }
        }
    }
}