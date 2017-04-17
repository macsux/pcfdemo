using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace FortunesWebApiService
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = GetServerUrls(args).FirstOrDefault() ?? "http://localhost:9000/";
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {

                Console.ReadLine();
            }


        }
        private static string[] GetServerUrls(string[] args)
        {
            List<string> urls = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                if ("--server.urls".Equals(args[i], StringComparison.OrdinalIgnoreCase))
                {
                    urls.Add(args[i + 1]);
                }
            }
            var port = Environment.GetEnvironmentVariable("PORT");

            if (!urls.Any() && !string.IsNullOrWhiteSpace(port))
            {
                var url = $"http://localhost:{port}/";
                Console.WriteLine(url);
                urls.Add(url);
            }
            return urls.ToArray();
        }
    }
}
