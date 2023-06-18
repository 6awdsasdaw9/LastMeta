using System;
using UnityEngine;

namespace Code.Services
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _cooldown = 1;
        private float _currentCooldown;

        public float Normalize => _currentCooldown / _cooldown; 
        public void SetTime(float value)
        {
            _cooldown = value;
        }

        public bool IsUp()
        {
            return _currentCooldown <= 0;
        }

        public void ResetCooldown()
        {
            _currentCooldown = _cooldown;
        }

        public bool UpdateCooldown()
        {
            if (!IsUp())
            {
                _currentCooldown -= Time.deltaTime;
                return false;
            }
            return true;
        }
    }
}