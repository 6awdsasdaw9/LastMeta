using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Triggers
{
    public class TriggerObserverAdapter : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _delay;
        [SerializeField] private float _cooldown;
        [SerializeField] private FollowTriggerObserver[] _followTriggerObserver;

        private CancellationTokenSource _tokenSource;
        private bool _hasReactionTarget;

        private void Awake()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void OnDisable()
        {
            SwitchFollowOff();
        }

        #region Triggers

        private void TriggerEnter(Collider obj)
        {
            if (_hasReactionTarget)
                return;

            _hasReactionTarget = true;
            _tokenSource = new CancellationTokenSource();
            SwitchFollowOnAfterDelay().Forget();
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasReactionTarget)
                return;

            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            _hasReactionTarget = false;
            SwitchFollowOffAfterCooldown().Forget();
        }

        #endregion

        #region Coroutines

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
            foreach (var f in _followTriggerObserver)
                f.enabled = true;
        }

        private void SwitchFollowOff()
        {
            foreach (var f in _followTriggerObserver)
                f.enabled = false;
        }

        #endregion
    }
}