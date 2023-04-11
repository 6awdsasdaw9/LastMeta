using System;
using Code.Data.Configs;
using Code.Data.GameData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroCollision : MonoBehaviour
    {
        public bool onGround { get; private set; }
        public bool underCeiling;
        
        [SerializeField] private HeroMovement _hero;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private LayerMask _groundLayer;

        [Space, Title("Collider Settings")]
        [SerializeField] private float _groundLength = 0.95f;
        [SerializeField] private float _ceilingLength = 0.95f;
        [SerializeField] private Vector3 _colliderOffset;
        private PhysicMaterial _noFrictionMaterial;
        private PhysicMaterial _frictionMaterial;

        [Inject]
        private void Construct(GameConfig gameConfig)
        {
            _frictionMaterial = gameConfig.heroConfig.FrictionMaterial;
            _noFrictionMaterial = gameConfig.heroConfig.NoFrictionMaterial;
        }

        private void Start()
        {
            SetNoFrictionPhysicsMaterial();
        }

        private void Update()
        {
            onGround = GroundCheck();
            underCeiling = CeilingCheck();
            SetCollision();
        }

        public void SetFrictionPhysicsMaterial() => 
            _collider.material = _frictionMaterial;

        public void SetNoFrictionPhysicsMaterial() =>
            _collider.material = _noFrictionMaterial;

        private void SetCollision()
        {
            if (_hero.isCrouch)
            {
                EnableHorizontalCollider();
            }
            else
            {
                EnableVerticalCollider();
            }
        }
        
        private void EnableHorizontalCollider()
        {
            _collider.direction = 0;
            _collider.center = new Vector3(0, 0.05f, 0);
            _collider.height = 1f;
        }

        private void EnableVerticalCollider()
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