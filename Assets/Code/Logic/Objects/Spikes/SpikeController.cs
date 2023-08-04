using Code.Character.Common.CommonCharacterInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Objects.Animations;
using Code.Logic.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Spikes
{
    public class SpikeController : MonoBehaviour, IDisabledComponent
    {
        [SerializeField] private StartStopAnimation _animation;
        [SerializeField] private TriggerObserver _damageTrigger;
        [SerializeField] private float _damage;
        private IHero _hero;

        [Inject]
        private void Construct(IHero hero)
        {
            _hero = hero;
        }

        public void DisableComponent()
        {
            _damageTrigger.OnEnter -= OnTriggerEnter;
            _animation.PlayStop();
        }

        public void EnableComponent()
        {
            _damageTrigger.OnEnter += OnTriggerEnter;
            _animation.PlayStart();
        }

        private void OnTriggerEnter(Collider obj)
        {
            _hero.Health.TakeDamage(_damage);
        }
    }
}