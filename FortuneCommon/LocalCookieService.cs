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

            "I think Microsoft named .Net so it wouldn’t show up in a Unix directory listing.",

            "Some things Man was never meant to know. For everything else, there's Google.",

            "Failure is not an option -- it comes bundled with Windows.",

            "Computer games don't affect kids; I mean if Pac-Man affected us as kids, we'd all be running around in darkened rooms, munching magic pills and listening to repetitive electronic music.",

            "COBOL programmers understand why women hate periods.",

            "Artificial Intelligence usually beats natural stupidity.",

            "To err is human... to really foul up requires the root password.",

            "Like car accidents, most hardware problems are due to driver error.",

            "If at first you don't succeed; call it version 1.0",

            "If Python is executable pseudocode, then perl is executable line noise.",

            "Programmers are tools for converting caffeine into code.",

            "Why do we want intelligent terminals when there are so many stupid users?",

            "I can't uninstall it, there seems to be some kind of 'Uninstall Shield'.",

            "See daddy ? All the keys are in alphabetical order now.",

            "Hey! It compiles! Ship it!",

            "SUPERCOMPUTER: what it sounded like before you bought it.",

            "Yo moma is like HTML: Tiny head, huge body.",

            "Windows Vista: It's like upgrading from Bill Clinton to George W. Bush.",

            "The more I C, the less I see.",

            "Life would be so much easier if we only had the source code.",

            "My software never has bugs. It just develops random features.",

            "The only problem with troubleshooting is that sometimes trouble shoots back.",

            "Crap... Someone knocked over my recycle bin... There's icons all over my desktop...",

            "Relax, its only ONES and ZEROS !",

            "rm -rf /bin/laden",

            "I don't care if you ARE getting a PhD in it ! Get away from that damn computer and go find a woman !",

            "The great thing about Object Oriented code is that it can make small, simple problems look like large, complex ones.",

            "If brute force doesn't solve your problems, then you aren't using enough.",

            "Programming is like sex, one mistake and you have to support it for the rest of your life.",

            "Unix is user-friendly. It's just very selective about who its friends are.",

            "Microsoft: 'You've got questions. We've got dancing paperclips.'",

            "I'm not anti-social; I'm just not user friendly",

            "The world is coming to an end... SAVE YOUR BUFFERS !",

            "If you don't want to be replaced by a computer, don't act like one.",

            "Better to be a geek than an idiot.",

            "I went to a gentleman's cybercafe — and they offered me a 'laptop dance'.",

            "After Perl everything else is just assembly language.",

            "The Internet: where men are men, women are men, and children are FBI agents.",

            "There are 10 types of people in the world: those who understand binary, and those who don't.",

            "Difference between a virus and windows ? Viruses rarely fail.",

            "Hacking is like sex. You get in, you get out, and hope that you didn't leave something that can be traced back to you.",

            "1f u c4n r34d th1s u r34lly n33d t0 g37 l41d",

            @"Helpdesk: There is an icon on your computer labeled 'My Computer'. Double click on it., User: What's your computer doing on mine?",

            "I think Microsoft named .Net so it wouldn’t show up in a Unix directory listing.",

            "If debugging is the process of removing bugs, then programming must be the process of putting them in.",

            "Computer dating is fine, if you're a computer.",

            "Any fool can use a computer. Many do.",

            "Hardware: The parts of a computer system that can be kicked.",

            "Those who can't write programs, write help files.",

            "You know you're a geek when... You try to shoo a fly away from the monitor with your cursor. That just happened to me. It was scary.",

            "Computer language design is just like a stroll in the park. Jurassic Park, that is.",
        };
        public Task<string> GetCookie()
        {
            return Task.FromResult(Cookies[(new Random()).Next(0, Cookies.Length - 1)]);
        }
    }
}