using System;
using UnityEngine;

namespace Code.Services
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _cooldown = 1;
        private float _currentCooldown;

        public bool IsUp() =>
            _currentCooldown <= 0;

        public void ResetCooldown() => 
            _currentCooldown = _cooldown;

        public void UpdateCooldown()
        {
            if (!IsUp())
                _currentCooldown -= Time.deltaTime;
        }
    }
}