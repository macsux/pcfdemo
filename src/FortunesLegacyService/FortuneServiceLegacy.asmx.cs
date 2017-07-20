using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Services;
using FortuneCommon;
using MySql.Data.MySqlClient;

namespace FortunesLegacyService
{
    /// <summary>
    /// Summary description for FortuneServiceLegacy
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FortuneServiceLegacy : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetCookie()
        {
//            return new LocalCookieService().GetCookie();
            var connection = Global.DatabaseFactory();
            var adapter = new MySqlDataAdapter("select * from FortuneCookies", connection);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);
            var randomCookieIndex = new Random().Next(0, dataTable.Rows.Count - 1);
            var cookie = (string)dataTable.Rows[randomCookieIndex]["Cookie"];
            return cookie;
        }
    }
}
