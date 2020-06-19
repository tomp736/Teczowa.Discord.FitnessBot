using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System;

namespace fitnessbot.console
{
    public partial class QuoteCommandModule : BaseModule
    {
        private DiscordClient _client;
        protected override void Setup(DiscordClient client)
        {
            _client = client;
        }

        public class Quote
        {
            public string text { get; set; }
            public string author { get; set; }
        }

        private static List<Quote> _quotesResponse = null;

        [Command("quote")]
        public async Task QuoteCommand(CommandContext ctx)
        {
            await ExecuteQuote(ctx).ConfigureAwait(false);
        }

        public static async Task ExecuteQuote(CommandContext ctx)
        {
            if (_quotesResponse == null)
            {
                WebClient webClient = new WebClient();
                string jokeJson = webClient.DownloadString("https://type.fit/api/quotes");
                _quotesResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Quote>>(jokeJson);
            }


            if (_quotesResponse != null && _quotesResponse.Any())
            {
                Random rnd = new Random();
                int quoteitem = rnd.Next(_quotesResponse.Count - 1);
                Quote quote = _quotesResponse.Skip(quoteitem).Take(1).FirstOrDefault();
                if (quote != null)
                {
                    await ctx.Channel.SendMessageAsync($"{quote.author} ~ \"{quote.text}\"").ConfigureAwait(false);
                }
            }
        }
    }
}