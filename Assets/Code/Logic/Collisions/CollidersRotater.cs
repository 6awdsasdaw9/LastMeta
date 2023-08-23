using UnityEngine;

namespace Code.Logic.Collisions
{
    public class CollidersRotater: MonoBehaviour
    {
        [SerializeField] private Collider[] _colliders;
        
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