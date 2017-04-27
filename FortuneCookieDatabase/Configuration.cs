using System.Data.Entity.Migrations;
using System.Linq;
using FortuneCommon;

namespace FortuneCookieDatabase
{
    internal class Configuration : DbMigrationsConfiguration<FortuneCookieDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(FortuneCookieDbContext context)
        {
            if (!context.FortuneCookies.Any())
            {
                foreach (var cookie in LocalCookieService.Cookies)
                {
                    context.FortuneCookies.AddOrUpdate(x => x.Cookie, new FortuneCookie() {Cookie = cookie, Language = "en"});
                }
            }
        }
    }
}