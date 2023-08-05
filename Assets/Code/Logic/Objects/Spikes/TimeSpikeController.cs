using Code.Audio.AudioEvents;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Common;
using Code.Logic.Common.Interfaces;
using Code.Logic.Objects.Animations;
using Code.Logic.TimingObjects.TimeObserverses;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Spikes
{
    public class TimeSpikeController : TimeObserver, IDisabledComponent
    {
        [SerializeField] private SpikeType _type;
        [SerializeField] private StartStopAnimation _animation;
        [SerializeField] private TriggerObserver _damageTrigger;
        [SerializeField] private SpikeData _data;
        private HeroPusher _pusher;
        
        private AudioEvent _audioEvent;
        private IHero _hero;

        private bool _isActive;

        [Inject]
        private void Construct(DiContainer container)
        {
            _hero = container.Resolve<IHero>();
            _pusher = new HeroPusher(owner: this.transform, hero: _hero, pushData: _data.PushData);
        }
        
        protected override void StartReaction()
        {
            EnableComponent();
        }
        
        protected override void EndReaction()
        {
            DisableComponent();
        }

        protected override void SetStartReaction()
        {
            _animation.PlayStartIdle();
        }

        protected override void SetEndReaction()
        {
            _animation.PlayStopIdle();
        }

        public void DisableComponent()
        {
            _damageTrigger.OnEnter -= OnTriggerEnter;
            _animation.PlayStop();
            _isActive = false;
        }

        public void EnableComponent()
        {
            _damageTrigger.OnEnter += OnTriggerEnter;
            _animation.PlayStart();
            _isActive = true;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if(!_isActive)return;
            _hero.Health.TakeDamage(_data.Damage);
            _pusher.Push().Forget();
        }
    }
}