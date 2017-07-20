using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FortuneCommon;
using Pivotal.Discovery.Client;

namespace FortunesFormsUI
{
    
    public partial class _Default : Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("Cookies.aspx");
        }
 
    }

    
}