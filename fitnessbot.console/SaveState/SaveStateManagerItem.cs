using System;
using System.Linq;
using System.Timers;

namespace fitnessbot.console.userweight
{
    public class SaveStateManagerItem : SaveStateManagerItemBase
    {
        private static string _saveStateDataPath = "SaveStateData";
        
        public SaveStateManagerItem(string itemname)
        {
            if (string.IsNullOrEmpty(itemname))
                throw new ArgumentNullException("argument cannot be null");
            _itemName = itemname;
        }

        public void ConfigureSaveState<T>(T defaultState)
        {
            _currentState = LoadLastStateOrDefault(System.IO.Path.Join(_saveStateDataPath, _itemName), defaultState);
            _saveStateTimer = new System.Timers.Timer();
            _saveStateTimer.Interval = 1000;
            _saveStateTimer.Elapsed += OnElapsed;
            _saveStateTimer.Start();
        }

        private T LoadLastStateOrDefault<T>(string directory, T defaultValue)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
            else
            {
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(directory);
                foreach (var statefile in dirInfo.GetFiles().OrderByDescending(file => file.LastWriteTimeUtc))
                {
                    try
                    {
                        string filePath = System.IO.Path.Join(directory, statefile.Name);
                        var stateFileJson = System.IO.File.ReadAllText(filePath);
                        var userWeightInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stateFileJson);

                        if (userWeightInfo != null)
                        {
                            return userWeightInfo;
                        }

                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
            }
            return defaultValue;
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            if (isDirty == false)
                return;

            long fileTime = DateTime.UtcNow.ToFileTimeUtc();
            string filePath = System.IO.Path.Join(_saveStateDataPath, _itemName, $"{fileTime.ToString()}.json");
            System.IO.File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(_currentState));
            isDirty = false;
        }
    }
}