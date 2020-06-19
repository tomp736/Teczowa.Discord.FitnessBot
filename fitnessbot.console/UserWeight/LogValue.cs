using System;

namespace fitnessbot.console.userlog
{
    public class LogValue
    {
        public long timestamp { get; set; }
        public string unit { get; set; }
        public decimal value { get; set; }
    }
}