using UnityEngine;
using UnityEngine.Serialization;

//This script is used by both movement and jump to detect when the character is touching the ground

namespace Code.Character
{
    public class CharacterCollision : MonoBehaviour
    {
        public bool onGround { get; private set; }


        [FormerlySerializedAs("groundLength")]
        [Header("Collider Settings")]
        [SerializeField] private float _groundLength = 0.95f;
        [SerializeField] private Vector3 _colliderOffset;

        [Header("Layer Masks")]
        [SerializeField] private LayerMask _groundLayer;
 

        private void Update()
        {
            onGround = GroundCheck();
        }

        private bool GroundCheck() => 
            Physics2D.Raycast(transform.position + _colliderOffset, Vector2.down, _groundLength, _groundLayer) || 
            Physics2D.Raycast(transform.position - _colliderOffset, Vector2.down, _groundLength, _groundLayer);


        private void OnDrawGizmos()
        {
            if (onGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
            Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.down * _groundLength);
            Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.down * _groundLength);
        }

    }
}