using System;
using UnityEngine;

public class Day : MonoBehaviour
{

    private float _dayTime;
    private float _currentTime;
    private void Update()
    {
        ClockMovement();
    }

    private void ClockMovement()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dayTime)
            _currentTime = 0;
    }
}
