using System;
using System.Collections.Generic;

namespace fitnessbot.console.userweight
{
    public class UserWeightInfo
    {
        public string UserName { get; private set; }
        public Dictionary<long, WeightValue> _userWeights = new Dictionary<long, WeightValue>();

        public UserWeightInfo() { }
        public UserWeightInfo(string username)
        {
            UserName = username;
        }

        internal UserWeightResponse LogWeight(string value)
        {
            UserWeightResponse userWeightResponse = new UserWeightResponse();
            decimal dvalue;
            if(decimal.TryParse(value, out dvalue))
            {
                WeightValue weightValue = new WeightValue()
                {
                    unit = "lb",
                    weight = dvalue
                };
                _userWeights.Add(DateTime.UtcNow.ToFileTime(), weightValue);
                userWeightResponse.AddInfo($"{UserName} logged {dvalue.ToString()}lb");
            }
            else
            {
                userWeightResponse.AddError($"could not parse '{value}' as a valid weight");
            }
            return userWeightResponse;
        }

        internal UserWeightResponse ListWeight()
        {
            UserWeightResponse userWeightResponse = new UserWeightResponse();
            foreach(var weightValue in _userWeights)
            {
                userWeightResponse.AddInfo($"{UserName} logged {weightValue.Value.weight.ToString()}lb at {weightValue.Key}");
            }
            return userWeightResponse;
        }
    }
}