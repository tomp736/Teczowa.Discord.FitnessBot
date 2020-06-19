using System.Collections.Generic;
using fitnessbot.console.savestate;

namespace fitnessbot.console.userlog
{
    public class UserLogManager
    {
        private static UserLogManager _userLogManager = new UserLogManager();
        public static UserLogManager Service => _userLogManager;

        private Dictionary<string, UserLogInfo> _userLogInfo;
        private UserLogManager()
        {
            _userLogInfo = SaveStateManager.Service.ConfigureSaveState(this.GetType().Name, new Dictionary<string, UserLogInfo>());
        }

        public UserLogResponse LogPoint(string user,string name, string value)
        {
            UserLogResponse response = GetUserLogInfo(user).LogPoint(name, value);
            SaveStateManager.Service.StateChanged(this.GetType().Name);
            return response;
        }

        public UserLogResponse ShowLog(string user, string name)
        {
            return GetUserLogInfo(user).ShowLog(name);
        }

        private UserLogInfo GetUserLogInfo(string user)
        {
            UserLogInfo userLogInfo;
            if (!_userLogInfo.TryGetValue(user, out userLogInfo))
            {
                userLogInfo = new UserLogInfo(user);
                _userLogInfo.Add(user, userLogInfo);
            }

            return userLogInfo;
        }
    }
}