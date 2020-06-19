using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;
using System.ServiceModel.Syndication;
using System.IO;
using System.Linq;

namespace fitnessbot.console
{
    public partial class XkcdCommandModule : BaseModule
    {
        private DiscordClient _client;
        protected override void Setup(DiscordClient client)
        {
            _client = client;
        }

        [Command("xkcd")]
        public async Task XKCD(CommandContext ctx)
        {
            await SendXkcd(ctx).ConfigureAwait(false);
        }

        public static async Task SendXkcd(CommandContext ctx)
        {
            WebClient webClient = new WebClient();
            string feedString = webClient.DownloadString("https://xkcd.com/atom.xml");
            SyndicationFeed feed = null;
            using (StringReader stringReader = new StringReader(feedString))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader))
                {
                    feed = SyndicationFeed.Load(xmlReader);
                }
            }

            var item = feed.Items.FirstOrDefault();
            if (item != null)
            {
                string pattern = @"src\s*=\s*""(.+?)""";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                var summary = item.Summary.Text;

                var match = rgx.Match(summary);

                // images.Add(matches[i].Value);
                await ctx.Channel.SendMessageAsync(match.Groups[1].Value).ConfigureAwait(false);
            }
        }
    }
}