using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.Logic.Collisions.Triggers
{
    /// <summary>
    /// The class that listens to TriggerObserver,
    /// this class is enabled when the player is inside the trigger
    /// and disabled when the player is out 
    /// </summary>
    public abstract class FollowTriggerObserver : MonoBehaviour, IDisabledComponent
    {
        public virtual void DisableComponent()
        {
            enabled = false;
        }

        public virtual void EnableComponent()
        {
            enabled = true;
        }
    }
}