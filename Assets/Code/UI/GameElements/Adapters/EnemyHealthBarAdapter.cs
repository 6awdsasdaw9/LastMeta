using Code.Character.Enemies;
using Code.Debugers;
using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.UI.GameElements.Adapters
{
    public class EnemyHealthBarAdapter : HealthBarAdapter
    {
        [SerializeField] private EnemyAudio _enemyAudio;
        [SerializeField] private EnemyHpBar _enemyHpBar;
        [SerializeField] private EnemyHealth _health;

        protected override HpBar hpBar => _enemyHpBar;
        protected override ICharacterHealth health => _health;


        protected override void SubscribeToEvents(bool flag)
        {
            base.SubscribeToEvents(flag);
            if (flag)
            {
                _health.OnHealthChanged += () => Logg.ColorLog($"{gameObject.name} o-o", ColorType.Olive);
                _health.OnHealthChanged += _enemyAudio.AudioPlayTakeDamage;
            }
            else
            {
                _health.OnHealthChanged -= _enemyAudio.AudioPlayTakeDamage;
            }
        }
    }
}