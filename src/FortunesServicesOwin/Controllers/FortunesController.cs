using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FortuneCookieDatabase;
using FortunesServicesOwin.Collapsers;
using Microsoft.Extensions.Logging;
using Steeltoe.CircuitBreaker.Hystrix.Strategy.ExecutionHook;
using Steeltoe.CircuitBreaker.Hystrix.Strategy.Options;

namespace FortunesWebApiService
{
    public class FortunesController : ApiController
    {
        private readonly Func<FortuneCookieDbContext> _contextFactory;
        private readonly Func<int,TestRequestCollapser> _getCookieCollapser;
        // GET api/values 
        public FortunesController(Func<FortuneCookieDbContext> contextFactory, Func<int, TestRequestCollapser> getCookieCollapser)
        {
            _contextFactory = contextFactory;
            _getCookieCollapser = getCookieCollapser;
        }

        public async Task<IList<string>> GetAllFortunes()
        {
            return await _contextFactory().FortuneCookies.Select(x => x.Cookie).ToListAsync();
        }


        [HttpGet]
        [ActionName("random")]
        public async Task<string> GetRandom()
        {

            var fortunes = await GetAllFortunes();
            var random = new Random().Next(0, fortunes.Count() - 1);
            return fortunes[random];
        }

        [HttpGet]
        public async Task<string> GetById(int id)
        {

            var result = await _getCookieCollapser(id).ToTask();
            return result;
        }
    }
}

