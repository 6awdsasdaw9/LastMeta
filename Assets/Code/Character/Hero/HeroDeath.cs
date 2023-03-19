using Code.Data.GameData;
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

        private GameObject _deathFx;

        [Inject]
        private void Construct(PrefabsData prefabsData)
        {
            _deathFx = prefabsData.fx_PlayerDeath;    
            _health.HealthChanged += HealthChanged;
        }

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _movement.enabled = false;
            _jump.enabled = false;
            _attack.enabled = false;

            _animator.PlayDeath();
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}
