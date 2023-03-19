using System.Linq;
using Code.Character.Hero;
using Code.Character.Interfaces;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _damage = 10;
        [SerializeField] private float _attackCooldown = 3f;
        [SerializeField] private float _cleavage = 1;
        [SerializeField] private float _effectiveHeight = 1;
        
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private readonly Collider[] _hits = new Collider[1];
        private Vector3 startPoint;
        public bool attackIsActive { get; private set; }
        
        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
                StartAttack();
        }

        private void StartAttack()
        {
            _animator.PlayAttack();
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _cleavage, 1);
            if (!Hit(out Collider hit))
                return;
            
            if (hit.gameObject.TryGetComponent(out IHealth heroHealth))
            {
                heroHealth.TakeDamage(_damage);
            }
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() =>
            attackIsActive = false;

        public void EnableAttack() =>
            attackIsActive = true;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + _effectiveHeight, transform.position.z);

        private void UpdateCooldown()
        {
            if (!CoolDownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            attackIsActive && !_isAttacking && CoolDownIsUp();

        private bool CoolDownIsUp() =>
            _currentAttackCooldown <= 0;
    }
    
}