using FortuneCommon;

namespace FortuneCookieDatabase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FortuneCookieDatabase.FortuneCookieDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FortuneCookieDatabase.FortuneCookieDbContext context)
        {
            
            if (!context.FortuneCookies.Any())
            {
                foreach (var cookie in LocalCookieService.Cookies)
                {
                    context.FortuneCookies.AddOrUpdate(x => x.Cookie, new FortuneCookie() { Cookie = cookie, Language = "en" });
                }
            }
        }
    }
}
