using System.Data.Entity.Migrations;
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
            foreach (var cookie in LocalCookieService.Cookies)
            {
                context.FortuneCookies.AddOrUpdate(x => x.Cookie, new FortuneCookie() {Cookie = cookie, Language = "en"});
            }
        }
    }
}