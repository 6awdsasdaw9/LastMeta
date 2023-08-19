using UnityEngine;

namespace Code.Logic.Collisions
{
    public class CollisionsController: MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;
        
        public void SetActive(bool isActive)
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = isActive;
            }
        }
        public void Flip()
        {
            foreach (var collider in _colliders)
            {
                var position = new Vector3(-collider.transform.localPosition.x, collider.transform.localPosition.y, 0);
                collider.transform.localPosition = position;
            }
        }
    }
}