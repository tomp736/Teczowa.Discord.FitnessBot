using System;
using System.Collections.Generic;

namespace fitnessbot.console.userlog
{
    public class UserLogInfo
    {
        public string UserName { get; set; }
        public Dictionary<string, List<LogValue>> _userLogs = new Dictionary<string, List<LogValue>>();

        public UserLogInfo() { }
        public UserLogInfo(string username)
        {
            UserName = username;
        }

        public static string GetDefaultUnit(string logTypeName)
        {
            if (logTypeName == "weight")
                return "lb";
            if (logTypeName == "sleep")
                return "hr";
            return "";
        }

        internal UserLogResponse LogPoint(string logTypeName, string value)
        {
            List<LogValue> logValues = GetLogValues(logTypeName);

            UserLogResponse userLogResponse = new UserLogResponse();
            decimal dvalue;
            if (decimal.TryParse(value, out dvalue))
            {
                LogValue LogValue = new LogValue()
                {
                    timestamp = DateTime.UtcNow.ToFileTimeUtc(),
                    unit = GetDefaultUnit(logTypeName),
                    value = dvalue
                };
                logValues.Add(LogValue);
                userLogResponse.AddInfo($"{UserName} logged {LogValue.value.ToString()}{LogValue.unit}");
            }
            else
            {
                userLogResponse.AddError($"could not parse '{value}' as a valid Log");
            }
            return userLogResponse;
        }

        internal UserLogResponse ShowLog(string logTypeName)
        {
            UserLogResponse userLogResponse = new UserLogResponse();
            List<LogValue> logValues = GetLogValues(logTypeName);            
            foreach (var LogValue in logValues)
            {
                userLogResponse.AddInfo($"Logged {LogValue.value.ToString()}{LogValue.unit} at {LogValue.timestamp}");
            }
            return userLogResponse;
        }

        private List<LogValue> GetLogValues(string logTypeName)
        {
            List<LogValue> logValues;
            if (!_userLogs.TryGetValue(logTypeName, out logValues))
            {
                logValues = new List<LogValue>();
                _userLogs.Add(logTypeName, logValues);
            }

            return logValues;
        }
    }
}