using System;
using System.Threading.Tasks;

namespace FortuneCommon
{
    public class LocalCookieService : ICookieService
    {
        public static readonly string[] Cookies =
        {
            "They don't make bugs like Bunny anymore.",
            "A programming language is low level when its programs require attention to the irrelevant",
            "I have always wished for my computer to be as easy to use as my telephone; my wish has come true because I can no longer figure out how to use my telephone.",
            "I think Microsoft named .Net so it wouldn’t show up in a Unix directory listing."
        };
        public Task<string> GetCookie()
        {
            return Task.FromResult(Cookies[(new Random()).Next(0, Cookies.Length - 1)]);
        }
    }
}