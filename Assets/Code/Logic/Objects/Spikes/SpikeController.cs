using Code.Audio.AudioEvents;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Common.Interfaces;
using Code.Logic.Objects.Animations;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Spikes
{
    public class SpikeController : MonoBehaviour, IDisabledComponent
    {
        [SerializeField] private StartStopAnimation _animation;
        [SerializeField] private TriggerObserver _damageTrigger;

        private SpikeData _data;
        private AudioEvent _audioEvent;
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
            _hero.Health.TakeDamage(_data.Damage);
        }
    }
}