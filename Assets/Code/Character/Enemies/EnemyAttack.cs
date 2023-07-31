using System;
using System.Linq;
using Code.Character.Hero;
using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using Code.Services;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Cooldown _attackCooldown;
        
        //Todo берем данные из конфига
        [SerializeField] private float _damage = 10;
        [SerializeField] private float _cleavage = 1;
        [SerializeField] private float _effectiveHeight = 1;
        [SerializeField] private float _pushForce = 3;


        private IHero _hero;
        private int _layerMask;
        private Vector3 _startPoint;
        private readonly Collider[] _hits = new Collider[1];
        
        
        private bool _isAttacking;
        private bool _attackIsActive;
        

        [Inject]
        private void Construct(IHero hero)
        {
            _hero = hero;
            _layerMask =  LayerMask.GetMask(Constants.HeroLayer);
        }
        
        private void Update()
        {
            if (CanAttack()) StartAttack();
        }

        private void OnDisable()
        {
            if (_isAttacking)
            {
                _hero.Movement.SetSupportVelocity(Vector2.zero);
            }
            _attackCooldown.ResetCooldown();
        }

        private void StartAttack()
        {
            if (_isAttacking) return;

            _isAttacking = true;
            _animator.PlayAttack();
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _cleavage, 1);
            if (!Hit(out Collider hit)) return;
            _hero.Health.TakeDamage(_damage);
            Push().Forget();
        }

        private async UniTaskVoid Push()
        {
            if (_pushForce == 0) return;
            _hero.Movement.SetSupportVelocity((transform.position - _hero.Transform.position) * _pushForce);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _hero.Movement.SetSupportVelocity(Vector2.zero);
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttackEnded()
        {
            _attackCooldown.ResetCooldown();
            _isAttacking = false;
        }

        public void DisableAttack() => _attackIsActive = false;
        public void EnableAttack() => _attackIsActive = true;

        private bool Hit(out Collider hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() => new(transform.position.x, transform.position.y + _effectiveHeight, transform.position.z);
        private bool CanAttack() => _attackIsActive && !_isAttacking && _attackCooldown.IsUp();
    }
}