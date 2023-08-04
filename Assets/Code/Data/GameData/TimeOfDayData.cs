using System;

namespace Code.Data.GameData
{
    public enum TimeOfDay
    {
        Morning,
        Evening,
        Night
    }
    
    [Serializable]
    public class TimeData
    {
        public float Seconds;
        public int Day;
        public string TimeOfDay;
    }
}