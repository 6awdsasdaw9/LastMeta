using System;
using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Services.EventsSubscribes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Logic.CameraLogic
{
    public class CameraStateController : MonoBehaviour, IEventsSubscriber
    {
        [SerializeField] private GameCameraFollow _gameCameraFollow;
        private EventsFacade _eventsFacade;
        private IHero _hero;

        private CancellationTokenSource _cts;
        
        [Inject]
        private void Construct(EventsFacade eventsFacade, IHero hero)
        {
            _eventsFacade = eventsFacade;
            _hero = hero;
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            _cts?.Cancel();
            SubscribeToEvents(false);
        }
        
        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _hero.Health.OnHealthChanged += OnHealthChanged;
            }
            else
            {
                _hero.Health.OnHealthChanged -= OnHealthChanged;
            }
        }

        private void OnHealthChanged()
        {
            SetBonusSpeed(0.65f, 0.7f).Forget();
        }

        private async UniTaskVoid SetBonusSpeed(float value, float duration)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            
            _gameCameraFollow.SetBonusSpeed(value);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: _cts.Token);
            _gameCameraFollow.ResetBonusSpeed();
        }
    }
}