using System;
using System.Linq;
using Code.Audio.AudioEvents;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Common;
using Code.Logic.Objects.Animations;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Spikes
{
    public class MissileSpikeController: MonoBehaviour
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

        private void OnEnable()
        {
            _animation.PlayStopIdle();
        }

        public  void StartReaction(Vector3 position)
        {
            transform.position = position;
            EnableComponent();
        }
        
        public  void EndReaction()
        {
            DisableComponent();
        }


        private void EnableComponent()
        {
            _damageTrigger.OnEnter += OnTriggerEnter;
            _animation.PlayStart();
            _audioEvent.PlayAudioEvent(_data.AudioData.EnableAudioEvent);
            _isActive = true;
        }

        private void DisableComponent()
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
            _pusher.Push().Forget();
        }
    }
}