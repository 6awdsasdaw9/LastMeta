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
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroJump _jump;
        [SerializeField] private HeroAttack _attack;

        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Rigidbody _rb;
        
        private GameObject _deathFx;
        private bool _isDeath;
        private MovementLimiter _limiter;

        [Inject]
        private void Construct(PrefabsData prefabsData,MovementLimiter limiter)
        {
            _deathFx = prefabsData.fx_PlayerDeath;    
            _health.HealthChanged += HealthChanged;
            _limiter = limiter;
        }

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.Current <= 0 && !_isDeath)
                Die();
        }

        private void Die()
        {
            _isDeath = true;
            _limiter.DisableMovement();
            
            transform.position -= new Vector3(0,0,0.2f);
           
            _movement.enabled = false;
            _jump.enabled = false;
            _attack.enabled = false;
            
            _animator.PlayDeath();
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}
