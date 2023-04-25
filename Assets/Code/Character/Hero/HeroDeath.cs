using Code.Data.Configs;
using Code.Data.GameData;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;


namespace Code.Character.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroAnimator _animator;
    
        [Space, Title("Disable Components")]
        [SerializeField] private HeroCollision _collision;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroJump _jump;
        [SerializeField] private HeroAttack _attack;
        
        private GameObject _deathFx;
        private bool _isDeath;
        private MovementLimiter _limiter;

        [Inject]
        private void Construct(PrefabsData prefabsData,MovementLimiter limiter)
        {
            _deathFx = prefabsData.fx_PlayerDeath;    
            _health.HealthChanged += HealthChanged;
            _collision.OnWater += DeathOnWater;
            _limiter = limiter;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
            _collision.OnWater -= DeathOnWater;
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0 && !_isDeath)
                Die();
        }

        private void Die()
        {
            DisableHero();

            _animator.PlayDeath();
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }

        private void DeathOnWater()
        {
            DisableHero();
            _animator.PlayDeathOnWater();
        }

        private void DisableHero()
        {
            _isDeath = true;
            _limiter.DisableMovement();


            _movement.enabled = false;
            _jump.enabled = false;
            _attack.enabled = false;

            transform.position = new Vector3(transform.position.x, transform.position.y, Constants.twoLayer);
        }
    }
}
