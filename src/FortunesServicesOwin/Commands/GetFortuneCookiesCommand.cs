using System.Collections.Generic;
using System.Linq;
using FortuneCookieDatabase;
using Steeltoe.CircuitBreaker.Hystrix;

namespace FortunesServicesOwin.Commands
{
    public class GetFortuneCookiesCommand : HystrixCommand<List<FortuneCookie>>
    {
        List<int> _ids;
        protected FortuneCookieDbContext _context;

        public GetFortuneCookiesCommand(FortuneCookieDbContext context, List<int> ids) : base(new HystrixCommandOptions(HystrixCommandGroupKeyDefault.AsKey("Group"), HystrixCommandKeyDefault.AsKey("Test")))
        {
            _context = context;
            _ids = ids;
        }

        protected override List<FortuneCookie> Run()
        {

            return _context.FortuneCookies.Where(x => _ids.Contains(x.Id)).ToList();
        }

        protected override List<FortuneCookie> RunFallback()
        {
            return new List<FortuneCookie>();
        }
    }
}