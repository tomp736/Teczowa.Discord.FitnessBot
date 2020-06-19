using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fitnessbot.console.userweight
{
    public class UserWeightResponse
    {
        public List<string> ErrorMessageCollection {get;set;} = new List<string>();
        public List<string> InfoMessageCollection {get;set;} = new List<string>();

        public void AddInfo(string message) => InfoMessageCollection.Add(message);
        public void AddError(string message) => ErrorMessageCollection.Add(message);

        internal async Task WriteTo(DiscordChannel channel)
        {
            List<string> allMessage = ErrorMessageCollection.Union(InfoMessageCollection).ToList();
            int current = 0;
            while(current < allMessage.Count)
            {
                string message = string.Join("\n", allMessage.Skip(current).Take(50));
                await channel.SendMessageAsync($"{message}").ConfigureAwait(false);
                current += 50;
            }
        }
    }
}