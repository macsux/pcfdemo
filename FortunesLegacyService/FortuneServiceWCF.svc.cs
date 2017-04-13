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
        public async Task<string> GetCookie()
        {
//            var context = new FortuneCookieDbContext("Server=localhost;Database=myDatabase;Uid=root;");
            var context = _dbContextFactory();
            //context.Database.Initialize(true);
            
            var cookies = await context.FortuneCookies.Select(x => x.Cookie).ToListAsync();
            var randomCookieIndex = new Random().Next(0, cookies.Count - 1);
            return cookies[randomCookieIndex];
//            return "";
        }


    }
}
