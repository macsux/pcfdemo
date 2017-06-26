using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FortunesUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiscoveryUrlResolver _resolver;

        public HomeController(DiscoveryUrlResolver resolver)
        {
            _resolver = resolver;
        }

        public IActionResult Index()
        {
            ViewBag.ServiceUrl = _resolver.LookupService("https://FortunesService/")
                .ToString()
                .Replace("http://","https://");
            return View();
        }


    }
}
