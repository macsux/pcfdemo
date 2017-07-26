using System.Collections.Generic;
using System.Linq;
using Antlr.Runtime.Misc;
using FortuneCookieDatabase;
using FortunesServicesOwin.Commands;
using Steeltoe.CircuitBreaker.Hystrix;

namespace FortunesServicesOwin.Collapsers
{
    public class TestRequestCollapser : HystrixCollapser<List<FortuneCookie>, string, int>
    {
        private readonly Func<List<int>, GetFortuneCookiesCommand> _commandFactory;

        public TestRequestCollapser(Func<List<int>,GetFortuneCookiesCommand> commandFactory, int id) : 
            base(GetOptions())
        {
            _commandFactory = commandFactory;
            RequestArgument = id;
        }

        private static HystrixCollapserOptions GetOptions()
        {
            var options = new HystrixCollapserOptions(HystrixCollapserKeyDefault.AsKey("collapser"));
            options.Scope = RequestCollapserScope.GLOBAL;
            return options;
        }

        protected override HystrixCommand<List<FortuneCookie>> CreateCommand(ICollection<ICollapsedRequest<string, int>> requests)
        {
            return _commandFactory(requests.Select(x => x.Argument).ToList());
            //return new HystrixCommand<List<Fortunecookies>>(new HystrixCommandOptions(HystrixCommandKeyDefault.AsKey("Test")), () => Run(requests), () => Fallback(requests));

        }

      
        protected override void MapResponseToRequests(List<FortuneCookie> batchResponse, ICollection<ICollapsedRequest<string, int>> requests)
        {
            var dic = batchResponse.ToDictionary(x => x.Id);
            foreach (var item in requests)
            {
                FortuneCookie cookie;
                if (dic.TryGetValue(item.Argument, out cookie))
                    item.Response = cookie.Cookie;
                else
                    item.Response = "Not available";
                item.Complete = true;
            }
        }

        public override int RequestArgument { get; }
    }
}