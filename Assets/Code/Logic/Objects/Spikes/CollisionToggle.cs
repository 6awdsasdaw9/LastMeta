using Code.Character.Common.CommonCharacterInterfaces;
using UnityEngine;

namespace Code.Logic.Objects.Spikes
{
    public class CollisionToggle: MonoBehaviour, IDisabledComponent
    {
        [SerializeField] private Collider _collider;

        public void DisableComponent()
        {
            _collider.enabled = false;
        }

        public void EnableComponent()
        {
            _collider.enabled = true;
        }
    }
}