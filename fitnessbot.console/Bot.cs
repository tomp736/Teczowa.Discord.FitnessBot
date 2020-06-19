using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;

namespace fitnessbot.console
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextModule Commands { get; private set; }

        internal async Task RunAsync()
        {
            var json = System.IO.File.ReadAllText("config.json");
            BotConfig botConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<BotConfig>(json);

            var config = new DiscordConfiguration
            {
                Token = botConfig.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;
            Client.TypingStarted += OnTypingStarted;
            Client.MessageCreated += OnMessageCreated;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefix = botConfig.Prefix,
                EnableDms = true,
                EnableMentionPrefix = true                                
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<LogCommandModule>();
            Commands.RegisterCommands<JokeCommandModule>();
            Commands.RegisterCommands<QuoteCommandModule>();
            Commands.RegisterCommands<SettingCommandModule>();
            Commands.RegisterCommands<XkcdCommandModule>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }


        private Task OnMessageCreated(MessageCreateEventArgs e)
        {
            return Task.CompletedTask;
        }

        private Task OnTypingStarted(TypingStartEventArgs e)
        {
            return Task.CompletedTask;
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
