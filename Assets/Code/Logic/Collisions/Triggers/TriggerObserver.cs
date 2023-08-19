using System;
using UnityEngine;

namespace Code.Logic.Collisions.Triggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : CollisionObserver
    {
        
        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other.gameObject);
        }
    }
}
