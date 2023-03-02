using Code.Data;
using Code.Debugers;
using UnityEngine;
using Zenject;


public class TimeOfDayController : ITickable
{
    private float _dayTime;
    private float _currentTime;

    private float _morningTime, _eveningTime, _nightTime;
    
    public delegate void TimeOfDayEvent();
    public event TimeOfDayEvent OnMorning;
    public event TimeOfDayEvent OnEvening;
    public event TimeOfDayEvent OnNight;


    [Inject]
    private void Construct(GameSettings settings)
    {
        _dayTime = settings.dayTime;
        _morningTime = 0;
        _eveningTime = _dayTime / 2;
        _nightTime = _dayTime - _dayTime / 3;
    }

    public void Tick()
    {
       ClockMovement();
       CheckTimeOfDay();
    }

    private void ClockMovement()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dayTime)
            _currentTime = 0;
    }

    private void CheckTimeOfDay()
    {
        if (_currentTime >= _morningTime && _currentTime < _eveningTime)
        {
            OnMorning?.Invoke(); // вызываем событие утра
        }
        else if (_currentTime >= _eveningTime && _currentTime < _nightTime)
        {
            OnEvening?.Invoke(); // вызываем событие вечера
        }
        else
        {
            OnNight?.Invoke(); // вызываем событие ночи
        }
    }
}