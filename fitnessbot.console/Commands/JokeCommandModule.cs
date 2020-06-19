using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Net;

namespace fitnessbot.console
{
    public partial class JokeCommandModule : BaseModule
    {
        private DiscordClient _client;
        protected override void Setup(DiscordClient client)
        {
            _client = client;
        }

        class JokeResponse
        {
            public string type { get; set; }
            public Joke value { get; set; }
        }

        public class Joke
        {
            public int id { get; set; }
            public string joke { get; set; }
            public string[] categories { get; set; }
        }

        [Command("joke")]
        public async Task JokeCommand(CommandContext ctx)
        {
            await SendJoke(ctx).ConfigureAwait(false);
        }

        public static async Task SendJoke(CommandContext ctx)
        {
            WebClient webClient = new WebClient();
            string jokeJson = webClient.DownloadString("http://api.icndb.com/jokes/random");

            JokeResponse jokeResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<JokeResponse>(jokeJson);

            if (jokeResponse.type == "success")
            {
                await ctx.Channel.SendMessageAsync(jokeResponse.value.joke).ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"Yeah... no. {jokeJson}").ConfigureAwait(false);
            }
        }
    }
}