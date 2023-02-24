using System.Linq;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator animator;
        public float damage = 10;
        public float attackCooldown = 3f;
        public float cleavage;
        public float effectiveDistance;

        
        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private Vector3 startPoint;
        private bool _attackIsActive;

        public void Construct(Transform transform1)
        {
            _heroTransform = transform1;
        }

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
            transform.LookAt(_heroTransform);
            animator.PlayAttack();
        }

        private void OnAttack()
        {
                PhysicsDebug.DrawDebug(StartPoint(), cleavage, 1);
            if (Hit(out Collider hit))
            {

                //hit.transform.GetComponent<HeroHealth>().TakeDamage(damage);
                if (hit.gameObject.TryGetComponent(out IHealth heroHealth))
                {
                //    heroHealth.TakeDamage(damage);
                    
                 
                }
            }
        }

        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() =>
            _attackIsActive = false;

        public void EnableAttack() =>
            _attackIsActive = true;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
            transform.forward * effectiveDistance;

        private void UpdateCooldown()
        {
            if (!CoolDownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CoolDownIsUp();

        private bool CoolDownIsUp() =>
            _attackCooldown <= 0;
    }

    internal interface IHealth
    {
    }
}