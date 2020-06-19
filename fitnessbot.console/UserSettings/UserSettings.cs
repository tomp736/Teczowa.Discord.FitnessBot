using System;

namespace fitnessbot.console.usersettings
{

    public class UserSettings
    {
        public string UserName { get; private set; }
        public string TimeZoneId { get; private set; }

        public UserSettings() { }
        public UserSettings(string username)
        {
            UserName = username;
        }

        internal SetUserSettingResponse SetValue(string name, string value)
        {          
            SetUserSettingResponse userSettingResponse = new SetUserSettingResponse();
            if (name == "tz")
            {
                try
                {
                    TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(value);
                    userSettingResponse.AddInfo($"timezone for {UserName} updated to {timeZoneInfo.Id}");
                    TimeZoneId = timeZoneInfo.Id;
                }
                catch
                {
                    userSettingResponse.AddError($"Invalid Time Zone.");
                    foreach(var tz in TimeZoneInfo.GetSystemTimeZones())
                    {
                        if(tz.Id.ToLower().Contains(value.ToLower()))
                        {
                            userSettingResponse.AddError($"Possible matches: {tz.Id}?");                            
                        }
                    }
                }
            }
            return userSettingResponse;
        }
    }
}