using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq;
using fitnessbot.console.userlog;

namespace fitnessbot.console
{
    public partial class LogCommandModule : BaseModule
    {
        private DiscordClient _client;
        protected override void Setup(DiscordClient client)
        {
            _client = client;
        }

        private static readonly string[] _singlePointLogItems = new string[] { "weight", "sleep" };

        [Command("log")]
        public async Task Log(CommandContext ctx)
        {
            var args = ctx.RawArgumentString.Trim().Split(" ");
            if(_singlePointLogItems.Contains(args[0]))
            {
                UserLogResponse response = null;

                if(args.Length == 2)
                {
                    if(args[1] == "show")
                    {
                        response = UserLogManager.Service.ShowLog(ctx.User.Username, args[0]);
                    }
                    else
                    {
                        response = UserLogManager.Service.LogPoint(ctx.User.Username, args[0], args[1]);
                    }        
                }                    
                else
                {
                    await ctx.Channel.SendMessageAsync("missing args").ConfigureAwait(false);
                }
                await response.WriteTo(ctx.Channel);
                return;
            }    
            await ctx.Channel.SendMessageAsync($"no logger of type '{args[0]}' exists").ConfigureAwait(false); 
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