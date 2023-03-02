using Code.Data;
using UnityEngine;
using Zenject;
public class TimeOfDayController : ITickable
{
    private float _dayTimeInSeconds;
    private float _currentSecondsOfDay;
    public float DayTimeNormalized => _currentSecondsOfDay / _dayTimeInSeconds;
    public TimeOfDay CurrentTimeOfDay { get; private set; }
    
    public delegate void TimeOfDayEvent();
    public event TimeOfDayEvent OnMorning;
    public event TimeOfDayEvent OnEvening;
    public event TimeOfDayEvent OnNight;


    [Inject]
    private void Construct(GameSettings settings)
    {
        _dayTimeInSeconds = settings.dayTimeInSeconds;
    }

    public void Tick()
    {
        ClockMovement();
        CheckTimeOfDay();
    }

    private void ClockMovement()
    {
        _currentSecondsOfDay += Time.deltaTime;
        if (_currentSecondsOfDay >= _dayTimeInSeconds)
            _currentSecondsOfDay = 0;
    }

    private void CheckTimeOfDay()
    {
        if (_currentSecondsOfDay < _dayTimeInSeconds * 0.25f)
        {
            SetCurrentTimeOfDay(TimeOfDay.Morning);
        }
        else if (_currentSecondsOfDay < _dayTimeInSeconds * 0.75f)
        {
            SetCurrentTimeOfDay(TimeOfDay.Evening);
        }
        else
        {
            SetCurrentTimeOfDay(TimeOfDay.Night);
        }
    }

    private void SetCurrentTimeOfDay(TimeOfDay newTimeOfDay)
    {
        if (newTimeOfDay == CurrentTimeOfDay) return;

        CurrentTimeOfDay = newTimeOfDay;

        switch (CurrentTimeOfDay)
        {
            case TimeOfDay.Morning:
                OnMorning?.Invoke();
                break;
            case TimeOfDay.Evening:
                OnEvening?.Invoke();
                break;
            case TimeOfDay.Night:
                OnNight?.Invoke();
                break;
        }
    }
}

public enum TimeOfDay
{
    Morning,
    Evening,
    Night
}