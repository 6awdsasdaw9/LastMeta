using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroCollision : MonoBehaviour, IHeroCollision
    {
        public bool OnGround { get; private set; }
        public bool UnderCeiling { get; private set; }
        public event Action OnWater;

        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _waterLayer;

        [Space, Title("Collider Settings")] 
        [SerializeField] private float _groundLength = 0.95f;

        [SerializeField] private float _ceilingLength = 0.95f;
        [SerializeField] private Vector3 _colliderOffset;
        private PhysicMaterial _noFrictionMaterial;
        private PhysicMaterial _frictionMaterial;

        private IHero _hero;

        #region Run Time

        [Inject]
        private void Construct(HeroConfig heroConfig)
        {
            _frictionMaterial = heroConfig.heroConfig.FrictionMaterial;
            _noFrictionMaterial = heroConfig.heroConfig.NoFrictionMaterial;
            _hero = GetComponent<IHero>();
        }

        private void Start()
        {
            SetNoFrictionPhysicsMaterial();
            CheckWater().Forget();
        }

        private void Update()
        {
            OnGround = GroundRaycast();
            UnderCeiling = CeilingRaycast();
            SetCollision();
        }
        
        #endregion

        #region Physics Material

        public void SetFrictionPhysicsMaterial() =>
            _collider.material = _frictionMaterial;

        public void SetNoFrictionPhysicsMaterial() =>
            _collider.material = _noFrictionMaterial;
        
        #endregion

        #region Collision Mode
        private void SetCollision()
        {
            if (_hero.Movement.IsCrouch)
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
        

        #endregion

        #region Check Surface
        private async UniTaskVoid CheckWater()
        {
            await UniTask.WaitUntil(WaterRaycast, cancellationToken: this.GetCancellationTokenOnDestroy());
            OnWater?.Invoke();
        }
        
        private bool GroundRaycast() => 
            Physics.Raycast(transform.position, Vector2.down, _groundLength, _groundLayer);

        private bool CeilingRaycast() =>
            Physics.Raycast(transform.position + _colliderOffset, Vector2.up, _ceilingLength, _groundLayer) ||
            Physics.Raycast(transform.position - _colliderOffset, Vector2.up, _ceilingLength, _groundLayer);

        private bool WaterRaycast() =>
            Physics.Raycast(transform.position, Vector2.down, _groundLength, _waterLayer);
        
        #endregion
        
        public void Disable()
        {
            _collider.enabled = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void Enable()
        {
            _collider.enabled = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            //ground
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundLength);
            //ceiling
            Gizmos.DrawLine(transform.position + _colliderOffset,
                transform.position + _colliderOffset + Vector3.up * _ceilingLength);
            Gizmos.DrawLine(transform.position - _colliderOffset,
                transform.position - _colliderOffset + Vector3.up * _ceilingLength);
        }
    }
}