using Newtonsoft.Json;

namespace fitnessbot.console
{
    public struct BotConfig
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}