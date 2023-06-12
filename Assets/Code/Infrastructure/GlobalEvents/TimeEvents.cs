using System;
using Code.Data.GameData;

namespace Code.Infrastructure.GlobalEvents
{
    public class TimeEvents
    {
        //Common
        private void StartTimeOfDayEvent(TimeOfDay timeOfDay) => OnStartTimeOfDay?.Invoke(timeOfDay);
        public event Action<TimeOfDay> OnStartTimeOfDay;

        private void EndTimeOfDayEvent(TimeOfDay timeOfDay) => OnEndTimeOfDay?.Invoke(timeOfDay);
        public event Action<TimeOfDay> OnEndTimeOfDay;
        
        //Morning
        public void StartMorningEvent()
        {
            StartTimeOfDayEvent(TimeOfDay.Morning);
            OnStartMorning?.Invoke();
        }
        public event Action OnStartMorning;

        public void EndMorningEvent()
        {
            EndTimeOfDayEvent(TimeOfDay.Morning);
            OnEndMorning?.Invoke();
        }
        public event Action OnEndMorning;
        
        //Evening
        public void StartEveningEvent()
        {
            StartTimeOfDayEvent(TimeOfDay.Evening);
            OnStartEvening?.Invoke();
        }
        public event Action OnStartEvening;
        
        public void EndEveningEvent()
        {
            EndTimeOfDayEvent(TimeOfDay.Evening);
            OnEndEvening?.Invoke();
        }
        public event Action OnEndEvening;
        
        //Night
        public void StartNightEvent()
        {
            StartTimeOfDayEvent(TimeOfDay.Night);
            OnStartNight?.Invoke();
        }
        public event Action OnStartNight;

        
        public void EndNightEvent()
        {
            EndTimeOfDayEvent(TimeOfDay.Night);
            OnEndNight?.Invoke();
        }
        public event Action OnEndNight;
        
        
    }
}