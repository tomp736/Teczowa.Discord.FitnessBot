using System.Collections.Generic;
using fitnessbot.console.savestate;

namespace fitnessbot.console.usersettings
{
    public class UserSettingsManager
    {
        private static UserSettingsManager _userSettingsManager = new UserSettingsManager();
        public static UserSettingsManager Service => _userSettingsManager;

        private Dictionary<string, UserSettings> _userSettings = new Dictionary<string, UserSettings>();
        private UserSettingsManager()
        {
            _userSettings = SaveStateManager.Service.ConfigureSaveState(this.GetType().Name, new Dictionary<string, UserSettings>());
        }

        public SetUserSettingResponse SetUserSetting(string user, string name, string value)
        {
            UserSettings userSettings;
            if (!_userSettings.TryGetValue(user, out userSettings))
            {
                userSettings = new UserSettings(user);
                _userSettings.Add(user, userSettings);
            }
            SetUserSettingResponse response = userSettings.SetValue(name, value);
            SaveStateManager.Service.StateChanged(this.GetType().Name);
            return response;
        }
    }
}