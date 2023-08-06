using System.Linq;
using Code.Audio.AudioEvents;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Debugers;
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
        
        private SpikeData _data;
        private HeroPusher _pusher;
        
        private readonly AudioEvent _audioEvent = new();
        private IHero _hero;

        private bool _isActive;

        [Inject]
        private void Construct(DiContainer container)
        {
            _hero = container.Resolve<IHero>();
            _data = container.Resolve<ObjectsConfig>().SpikesData.FirstOrDefault(s => s.Type == _type);
            _pusher = new HeroPusher(owner: transform, hero: _hero, pushData: _data.PushData);
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

        public void EnableComponent()
        {
            _damageTrigger.OnEnter += OnTriggerEnter;
            _animation.PlayStart();
            _audioEvent.PlayAudioEvent(_data.AudioData.EnableAudioEvent);
            _isActive = true;
        }

        public void DisableComponent()
        {
            _damageTrigger.OnEnter -= OnTriggerEnter;
            _audioEvent.PlayAudioEvent(_data.AudioData.DisableAudioEvent);
            _animation.PlayStop();
            _isActive = false;
        }

        private void OnTriggerEnter(Collider obj)
        {
            if(!_isActive)return;
            _hero.Health.TakeDamage(_data.Damage);
            _audioEvent.PlayAudioEvent(_data.AudioData.CollisionAudioEvent);
            Logg.ColorLog($"OnTriggerEnter -> Push force ",ColorType.Red);
         
            _pusher.Push();
        }
    }
}