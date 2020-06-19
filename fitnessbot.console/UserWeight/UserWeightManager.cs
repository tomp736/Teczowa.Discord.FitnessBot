using System.Collections.Generic;

namespace fitnessbot.console.userweight
{
    public class UserWeightManager
    {
        private static UserWeightManager _userWeightManager = new UserWeightManager();
        public static UserWeightManager Service => _userWeightManager;

        private Dictionary<string, UserWeightInfo> _userWeightInfo;
        private UserWeightManager()
        {
            _userWeightInfo = SaveStateManager.Service.ConfigureSaveState(this.GetType().Name, new Dictionary<string, UserWeightInfo>());
        }

        public UserWeightResponse LogUserWeight(string user, string value)
        {
            UserWeightResponse response = GetUserWeightInfo(user).LogWeight(value);
            SaveStateManager.Service.StateChanged(this.GetType().Name);
            return response;
        }

        public UserWeightResponse ListUserWeight(string user)
        {
            return GetUserWeightInfo(user).ListWeight();
        }

        private UserWeightInfo GetUserWeightInfo(string user)
        {
            UserWeightInfo userWeightInfo;
            if (!_userWeightInfo.TryGetValue(user, out userWeightInfo))
            {
                userWeightInfo = new UserWeightInfo(user);
                _userWeightInfo.Add(user, userWeightInfo);
            }

            return userWeightInfo;
        }
    }
}