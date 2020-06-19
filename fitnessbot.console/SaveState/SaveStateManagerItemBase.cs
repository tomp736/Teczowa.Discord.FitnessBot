using System;

namespace fitnessbot.console.savestate
{
    public class SaveStateManagerItemBase
    {
        protected string _itemName = "";
        protected bool isDirty = false;
        protected System.Timers.Timer _saveStateTimer;
        protected object _currentState;
        public object CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
                isDirty = true;
            }
        }

        internal void MarkDirty()
        {
            isDirty = true;
        }
    }
}