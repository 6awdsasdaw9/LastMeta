using Code.Audio.AudioEvents;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Common;
using Code.Logic.Objects.Animations;
using UnityEngine;

namespace Code.Logic.Objects.Spikes
{
    public class MissileSpikeController : MonoBehaviour
    {
        [SerializeField] private SpikeType _type;
        [SerializeField] private StartStopAnimation _animation;
        [SerializeField] private TriggerObserver _damageTrigger;
        [SerializeField] private SpikeData _data;
        private HeroPusher _pusher;

        private AudioEvent _audioEvent;
        private IHero _hero;

        private bool _isActive;

        public void StartReaction(Vector3 position)
        {
            transform.position = position;
            EnableComponent();
        }

        public void EndReaction()
        {
            DisableComponent();
        }


        private void DisableComponent()
        {
            _damageTrigger.OnEnter -= OnTriggerEnter;
            _animation.PlayStop();
            _isActive = false;
        }

        private void EnableComponent()
        {
            _damageTrigger.OnEnter += OnTriggerEnter;
            _animation.PlayStart();
            _isActive = true;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (!_isActive) return;
            _hero.Health.TakeDamage(_data.Damage);
            _pusher.Push().Forget();
        }
    }
}