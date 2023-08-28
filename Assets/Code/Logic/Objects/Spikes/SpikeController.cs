using System;
using System.Threading;
using Code.Audio.AudioEvents;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Common;
using Code.Logic.Objects.Animations;
using Code.Services;
using Code.Services.EventsSubscribes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Objects.Spikes
{
    [Serializable]
    public class SpikeController : IEventsSubscriber
    {
        [SerializeField] private StartStopAnimation _animation;
        [SerializeField] private TriggerObserver _damageTrigger;
        private Cooldown _cooldown;
        private SpikeData _data;
        private IHero _hero;

        private HeroPusher _pusher;
        private AudioEvent _audioEvent = new();

        private CancellationTokenSource _cts;

        private bool _isWatching;
        private bool _isAttacking;

        public void Init(Transform owner, IHero hero, SpikeData data)
        {
            _hero = hero;
            _data = data;
            _pusher = new HeroPusher(owner: owner.transform, hero: _hero, pushData: _data.PushData);
            _cooldown = new Cooldown();
            _cooldown.SetMaxTime(_data.Cooldown);
        }

        public void StartReaction()
        {
            SubscribeToEvents(true);
            _animation.PlayStart();
            _audioEvent.PlayAudioEvent(_data.AudioData.EnableAudioEvent);
        }

        public void EndReaction()
        {
            SubscribeToEvents(false);
            _isWatching = false;
            _audioEvent.PlayAudioEvent(_data.AudioData.DisableAudioEvent);
            _animation.PlayStop();
        }

        public void SubscribeToEvents(bool flag)
        {
            _damageTrigger.gameObject.SetActive(flag);

            if (flag)
            {
                _damageTrigger.OnEnter += OnEnter;
                _damageTrigger.OnExit += OnExit;
            }
            else
            {
                _damageTrigger.OnEnter -= OnEnter;
                _damageTrigger.OnExit -= OnExit;
            }
        }

        private void OnEnter(GameObject obj)
        {
            _isWatching = true;
            StartFollowingTrigger();
        }

        private void OnExit(GameObject obj)
        {
            _isWatching = false;
            _cts?.Cancel();
        }

        public void SetStartReaction()
        {
            _animation.PlayStartIdle();
        }

        public void SetEndReaction()
        {
            _animation.PlayStopIdle();
        }

        public void StartFollowingTrigger()
        {
            FollowingTrigger().Forget();
        }

        private async UniTaskVoid FollowingTrigger()
        {
            if (_isAttacking) return;

            _isAttacking = true;
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            _cooldown.SetZeroCooldown();

            await UniTask.Delay(
                TimeSpan.FromSeconds(_data.DelayAfterAttack),
                cancellationToken: _hero.Transform.gameObject.GetCancellationTokenOnDestroy());

            while (_isWatching)
            {
                await UniTask.WaitUntil(
                    () => _cooldown.IsUp(),
                    cancellationToken: _hero.Transform.gameObject.GetCancellationTokenOnDestroy());

                if (!_isWatching) break;

                AttackHero();
                _cooldown.SetMaxCooldown();
            }

            _isAttacking = false;
        }

        public void AttackHero()
        {
            _hero.Health.TakeDamage(_data.Damage);
            _audioEvent.PlayAudioEvent(_data.AudioData.CollisionAudioEvent);
            _pusher.Push();
        }
    }
}