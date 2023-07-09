using UnityEngine;

namespace Code.Logic.Objects
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private Vector2 _force = Vector2.up;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(_force, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, _force * 0.2f);
        }
    }
}