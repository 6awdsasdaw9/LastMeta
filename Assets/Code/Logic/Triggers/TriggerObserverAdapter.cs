using System.Collections;
using UnityEngine;

namespace Code.Logic.Triggers
{
    public class TriggerObserverAdapter : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _delay;
        [SerializeField] private float _cooldown;
        [SerializeField] private FollowTriggerObserver[] _followTriggerObserver;
        
        private Coroutine _reactionCoroutine;
        private bool _hasReactionTarget;
        private void Awake()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasReactionTarget)
                return;
            
            _hasReactionTarget = true;
            StopReactionCoroutine();
            StartCoroutine(SwitchFollowOnAfterDelay());
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasReactionTarget) 
                return;
            
            StopCoroutine(SwitchFollowOnAfterDelay());
            _reactionCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            _hasReactionTarget = false;
        }

        private IEnumerator SwitchFollowOnAfterDelay()
        {
            yield return new WaitForSeconds(_delay);
            SwitchFollowOn();
        }
        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void StopReactionCoroutine()
        {
            if (_reactionCoroutine == null) 
                return;
            StopCoroutine(_reactionCoroutine);
            _reactionCoroutine = null;
        }

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
    }
}