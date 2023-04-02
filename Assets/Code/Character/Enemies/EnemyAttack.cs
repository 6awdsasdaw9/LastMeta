using System.Linq;
using System.Threading.Tasks;
using Code.Character.Hero;
using Code.Character.Interfaces;
using Code.Debugers;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Cooldown _attackCooldown;
        [SerializeField] private float _damage = 10;
        [SerializeField] private float _cleavage = 1;
        [SerializeField] private float _effectiveHeight = 1;
        [SerializeField] private float _pushForce = 3;

        private HeroMovement _heroMovement;
        private IHealth _heroHealth;

     
        private bool _isAttacking;
        private int _layerMask;
        private readonly Collider[] _hits = new Collider[1];
        private Vector3 startPoint;
        public bool attackIsActive { get; private set; }

        [Inject]
        private void Construct(HeroMovement heroMovement)
        {
            _heroMovement = heroMovement;
            _heroHealth = heroMovement.GetComponent<IHealth>();

            _layerMask = 1 << LayerMask.NameToLayer(Constants.PlayerLayer);
        }


        private void Update()
        {
            _attackCooldown.UpdateCooldown();
            if (CanAttack())
                StartAttack();
        }

        private void StartAttack()
        {
            if (_isAttacking)
                return;

            _isAttacking = true;
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

            _heroHealth.TakeDamage(_damage);
            Push();
        }

        private async void Push()
        {
            if (_pushForce == 0)
                return;
            _heroMovement.SetSupportVelocity(-transform.localScale * _pushForce);
            await Task.Delay(500);
            _heroMovement.SetSupportVelocity(Vector2.zero);
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttackEnded()
        {
            _attackCooldown.ResetCooldown();
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



        private bool CanAttack() =>
            attackIsActive && !_isAttacking &&   _attackCooldown.IsUp();


    }
}