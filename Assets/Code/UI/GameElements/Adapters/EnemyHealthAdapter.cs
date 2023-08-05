using Code.Character.Enemies;
using UnityEngine;

namespace Code.UI.GameElements.Adapters
{
    public class EnemyHealthAdapter : HpBarAdapter
    {
        [SerializeField] private EnemyAudio _enemyAudio;
        protected new void Start()
        {
            base.Start();
            _hpBar = GetComponentInChildren<HpBar>();
            _health.OnHealthChanged += _enemyAudio.AudioPlayTakeDamage;
        }
        private void OnDisable()
        {
            _health.OnHealthChanged -= _enemyAudio.AudioPlayTakeDamage;
        }
    }
}