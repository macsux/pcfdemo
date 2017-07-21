using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using FortuneCommon;
using Pivotal.Discovery.Client;
using Newtonsoft.Json;

namespace FortunesFormsUI
{


    public class RestCookieClient : ICookieService
    {
        private DiscoveryHttpClientHandler _handler;
        private const string RANDOM_FORTUNE_URL = "http://FortunesService/api/fortunes/random";

        public RestCookieClient(IDiscoveryClient client)
        {
            _handler = new DiscoveryHttpClientHandler(client);
        }

        public async Task<string> GetCookieAsync()
        {
            var client = GetClient();
            
            var json = await client.GetStringAsync(RANDOM_FORTUNE_URL);
            var fortune = JsonConvert.DeserializeObject<string>(json);
            return fortune;

        }
        private System.Net.Http.HttpClient GetClient()
        {
            var client = new System.Net.Http.HttpClient(_handler, false);
            // distributed tracing headers
            var traceId = HttpContext.Current.Request.Headers["X-B3-TraceId"];
            if (traceId != null)
            {
                client.DefaultRequestHeaders.Add("X-B3-TraceId", traceId);
                client.DefaultRequestHeaders.Add("X-B3-ParentSpanId", HttpContext.Current.Request.Headers["X-B3-SpanId"]);
                client.DefaultRequestHeaders.Add("X-B3-SpanId", Guid.NewGuid().ToString());
            }
            return client;
        }

        public string GetCookie()
        {
            throw new NotImplementedException();
        }
    }
}