using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroCollision : MonoBehaviour
    {
        public bool onGround { get; private set; }
        public bool underCeiling;
        [SerializeField] private HeroMovement _hero;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private LayerMask _groundLayer;
        
        
        [Space, Header("Collider Settings")]
        [SerializeField] private float _groundLength = 0.95f;
        [SerializeField] private float _ceilingLength = 0.95f;
        
        [SerializeField] private Vector3 _colliderOffset;
        
        private void Update()
        {
            onGround = GroundCheck();
            underCeiling = CeilingCheck();
            SetCollision();
        }

        
        private void SetCollision()
        {
            if (_hero.isCrouch)
            {
                EnableCrouchCollider();
            }
            else
            {
                EnableStandCollider();
            }
        }

        private void EnableCrouchCollider()
        {
            _collider.direction = 0;
            _collider.center = new Vector3(0, 0.05f, 0);
            _collider.height = 1f;
        }

        private void EnableStandCollider()
        {
            _collider.direction = 1;
            _collider.center = new Vector3(0, 0.65f, 0);
            _collider.height = 1.5f;
        }

        private bool GroundCheck() =>
            Physics.Raycast(transform.position, Vector2.down, _groundLength, _groundLayer);


        private bool CeilingCheck()
        {
            return Physics.Raycast(transform.position  + _colliderOffset, Vector2.up, _ceilingLength, _groundLayer)||
                   Physics.Raycast(transform.position - _colliderOffset, Vector2.up, _ceilingLength, _groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green; 
            
            //ground
            Gizmos.DrawLine(transform.position, transform.position  + Vector3.down * _groundLength);
            //ceiling
            Gizmos.DrawLine(transform.position + _colliderOffset,transform.position + _colliderOffset + Vector3.up * _ceilingLength);
            Gizmos.DrawLine(transform.position - _colliderOffset,transform.position - _colliderOffset + Vector3.up * _ceilingLength);
        }

    }
}