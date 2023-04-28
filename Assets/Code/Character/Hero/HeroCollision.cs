using System;
using Code.Data.Configs;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private LayerMask _waterLayer;

        [Space, Title("Collider Settings")]
        [SerializeField] private float _groundLength = 0.95f;
        [SerializeField] private float _ceilingLength = 0.95f;
        [SerializeField] private Vector3 _colliderOffset;
        private PhysicMaterial _noFrictionMaterial;
        private PhysicMaterial _frictionMaterial;

        public Action OnWater;
        
        [Inject]
        private void Construct(GameConfig gameConfig)
        {
            _frictionMaterial = gameConfig.heroConfig.FrictionMaterial;
            _noFrictionMaterial = gameConfig.heroConfig.NoFrictionMaterial;
        }

        private void Start()
        {
            SetNoFrictionPhysicsMaterial();
            CheckWater();
        }
        
        private void Update()
        {
            onGround = GroundCheck();
            underCeiling = CeilingCheck();
            SetCollision();
        }

        public void DisableCollision()
        {
            _collider.enabled = false;
         
            if (TryGetComponent(out Rigidbody rigidbody)) 
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void SetFrictionPhysicsMaterial() => 
            _collider.material = _frictionMaterial;

        public void SetNoFrictionPhysicsMaterial() =>
            _collider.material = _noFrictionMaterial;

        private async UniTaskVoid CheckWater()
        {
            await UniTask.WaitUntil(WaterCheck, cancellationToken: this.GetCancellationTokenOnDestroy());
            OnWater?.Invoke();
        }
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

        private bool GroundCheck()
        {
            return Physics.Raycast(transform.position, Vector2.down, _groundLength, _groundLayer);
        }

        private bool CeilingCheck()
        {
            return Physics.Raycast(transform.position  + _colliderOffset, Vector2.up, _ceilingLength, _groundLayer)||
                   Physics.Raycast(transform.position - _colliderOffset, Vector2.up, _ceilingLength, _groundLayer);
        }
        
        private bool WaterCheck() => 
            Physics.Raycast(transform.position, Vector2.down, _groundLength, _waterLayer);

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