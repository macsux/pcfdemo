using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FortuneCommon;
using FortuneCookieDatabase;

namespace FortunesLegacyService
{

    public class FortuneServiceWCF : ICookieService
    {
        private readonly Func<FortuneCookieDbContext> _dbContextFactory;

        public FortuneServiceWCF(Func<FortuneCookieDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public FortuneCookieDbContext DbContext { get; set; }
        public async Task<string> GetCookieAsync()
        {
            var context = _dbContextFactory();
            var cookies = await context.FortuneCookies.Select(x => x.Cookie).ToListAsync();
            var randomCookieIndex = new Random().Next(0, cookies.Count - 1);
            return cookies[randomCookieIndex];
        }

        public string GetCookie()
        {
            var context = _dbContextFactory();
            var cookies = context.FortuneCookies.Select(x => x.Cookie).ToList();
            var randomCookieIndex = new Random().Next(0, cookies.Count - 1);
            return cookies[randomCookieIndex];

        }
    }
}
