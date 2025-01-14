using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Collisions.Triggers
{
    public class TriggerObserverAdapter : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _delay;
        [SerializeField] private float _cooldown;
        [SerializeField] private FollowTriggerObserver[] _enableObserver;
        [SerializeField] private FollowTriggerObserver[] _disableObserver;

        private CancellationTokenSource _tokenSource;
        private bool _hasReactionTarget;

        private void Awake()
        {
            _triggerObserver.OnEnter += OnEnter;
            _triggerObserver.OnExit += OnExit;

            SwitchFollowOff();
        }

        private void OnDisable()
        {
            SwitchFollowOff();
        }

        #region Triggers

        private void OnEnter(GameObject obj)
        {
            if (_hasReactionTarget) return;

            _hasReactionTarget = true;
            _tokenSource = new CancellationTokenSource();
            SwitchFollowOnAfterDelay().Forget();
        }

        private void OnExit(GameObject obj)
        {
            if (!_hasReactionTarget)
                return;

            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            _hasReactionTarget = false;
            SwitchFollowOffAfterCooldown().Forget();
        }

        #endregion

        #region Cooldowns

        private async UniTaskVoid SwitchFollowOnAfterDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: _tokenSource.Token);
            SwitchFollowOn();
        }

        private async UniTaskVoid SwitchFollowOffAfterCooldown()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: _tokenSource.Token);
            SwitchFollowOff();
        }

        #endregion

        #region Methods for followers

        private void SwitchFollowOn()
        {
            foreach (var observer in _enableObserver)
            {
                observer.EnableComponent();
            }

            foreach (var observer in _disableObserver)
            {
                observer.DisableComponent();
            }
        }

        private void SwitchFollowOff()
        {
            foreach (var observer in _enableObserver)
            {
                observer.DisableComponent();
            }
            
            foreach (var observer in _disableObserver)
            {
                observer.EnableComponent();
            }
        }

        #endregion
    }
}