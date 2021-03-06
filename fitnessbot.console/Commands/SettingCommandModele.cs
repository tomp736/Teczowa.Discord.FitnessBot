using DSharpPlus.CommandsNext;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq;
using fitnessbot.console.usersettings;

namespace fitnessbot.console
{
    public partial class SettingCommandModule : BaseModule
    {
        private DiscordClient _client;
        protected override void Setup(DiscordClient client)
        {
            _client = client;
        }

        [Command("set")]
        public async Task SetCommand(CommandContext ctx)
        {
            await ExecuteSet(ctx).ConfigureAwait(false);
        }

        private static async Task ExecuteSet(CommandContext ctx)
        {

            string[] args = ctx.Message.Content.Split(" ");
            SetUserSettingResponse response = new SetUserSettingResponse();
            if (args.Length >= 3)
            {
                response = UserSettingsManager.Service.SetUserSetting(ctx.User.Username, args[1], string.Join(" ", args.Skip(2).Take(100)));
            }
            else
            {
                response = new SetUserSettingResponse();
                response.AddError("Invalid argument count.");
            }
            await response.WriteTo(ctx.Channel);
        }
    }
}