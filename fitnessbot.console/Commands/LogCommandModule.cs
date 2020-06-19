using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using fitnessbot.console.userweight;

namespace fitnessbot.console
{
    public partial class LogCommandModule : BaseModule
    {
        private DiscordClient _client;
        protected override void Setup(DiscordClient client)
        {
            _client = client;
        }

        [Command("log")]
        public async Task Log(CommandContext ctx)
        {
            var args = ctx.RawArgumentString.Trim().Split(" ");
            if(args[0] == "weight")
            {
                UserWeightResponse response = null;

                if(args.Length == 2)
                {
                    if(args[1] == "show")
                    {
                        response = UserWeightManager.Service.ListUserWeight(ctx.User.Username);
                    }
                    else
                    {
                        response = UserWeightManager.Service.LogUserWeight(ctx.User.Username, args[1]);
                    }                    
                }                    
                else
                {
                    await ctx.Channel.SendMessageAsync("missing args").ConfigureAwait(false);
                }
                await response.WriteTo(ctx.Channel);
                return;
            }    
            await ctx.Channel.SendMessageAsync($"no logger configured for {args[0]}").ConfigureAwait(false); 
        }


        [Command("goodmorning")]
        public async Task GoodMorning(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Good morning!").ConfigureAwait(false);
            await XkcdCommandModule.SendXkcd(ctx).ConfigureAwait(false);
            await JokeCommandModule.SendJoke(ctx).ConfigureAwait(false);
            await QuoteCommandModule.ExecuteQuote(ctx).ConfigureAwait(false);
        }
    }
}