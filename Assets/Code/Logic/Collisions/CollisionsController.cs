using UnityEngine;

namespace Code.Logic.Collisions
{
    public class CollisionsController : MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;

        public void SetActive(bool isActive)
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = isActive;
            }
        }
        
        //--------------------------------------------------------------------------------------------------------------

        public void Flip()
        {
            foreach (var collider in _colliders)
            {
                var x = -collider.transform.localPosition.x;
                var y = collider.transform.localPosition.y;

                var localPosition = new Vector3(x, y, 0);
                collider.transform.localPosition = localPosition;
            }
        }

        public void Flip(bool isLookLeft)
        {
            foreach (var collider in _colliders)
            {
                var x = isLookLeft ? -collider.transform.localPosition.x : collider.transform.localPosition.x;
                var y = collider.transform.localPosition.y;

                var localPosition = new Vector3(x, y, 0);
                collider.transform.localPosition = localPosition;
            }
        }
    }
}