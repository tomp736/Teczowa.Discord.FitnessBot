using System;
using System.Collections.Generic;

namespace fitnessbot.console.userweight
{
    public class SaveStateManager
    {
        private static SaveStateManager _saveStateManager = new SaveStateManager();
        public static SaveStateManager Service => _saveStateManager;

        private Dictionary<string, SaveStateManagerItemBase> _saveStateManagerItems = new Dictionary<string, SaveStateManagerItemBase>();

        internal T ConfigureSaveState<T>(string name, T defaultState)
            where T : class
        {
            if(!_saveStateManagerItems.ContainsKey(name))
            {
                var item = new SaveStateManagerItem(name);
                item.ConfigureSaveState(defaultState);
                _saveStateManagerItems.Add(name, item);
            }
            return _saveStateManagerItems[name].CurrentState as T;
        }

        internal void StateChanged(string name)
        {
            if(_saveStateManagerItems.ContainsKey(name))
            {
               _saveStateManagerItems[name].MarkDirty();
            }
        }
    }
}