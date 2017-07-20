using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FortuneCookieDatabase;

namespace FortunesWebApiService
{
    public class FortunesController : ApiController
    {
        private readonly Func<FortuneCookieDbContext> _contextFactory;
        // GET api/values 
        public FortunesController(Func<FortuneCookieDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
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
            var random = new Random().Next(0, fortunes.Count()-1);
            return fortunes[random];
        }
    }
}
