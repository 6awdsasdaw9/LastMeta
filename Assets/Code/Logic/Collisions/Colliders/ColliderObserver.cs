using UnityEngine;

namespace Code.Logic.Collisions
{
    public class ColliderObserver : CollisionObserver
    {
        private void OnCollisionEnter(Collision collision)
        {
            OnEnter?.Invoke(collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnExit?.Invoke(collision.gameObject);
        }
    }
}