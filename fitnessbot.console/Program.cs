using System;
using System.Threading.Tasks;

namespace fitnessbot.console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Bottttttttt!");
            var bot = new Bot();
            await bot.RunAsync();
        }
    }
}
