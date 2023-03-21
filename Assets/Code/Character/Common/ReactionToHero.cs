using System.Collections;
using Code.Services;
using UnityEngine;

namespace Code.Character.Common
{
    public class ReactionToHero : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private float _cooldown;
        [SerializeField] private Follow[] _follows;
        
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;
        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) 
                return;
            
            _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            _hasAggroTarget = false;
        }

        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget)
                return;
            
            _hasAggroTarget = true;
            StopReactionCoroutine();
            SwitchFollowOn();
        }


        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void StopReactionCoroutine()
        {
            if (_aggroCoroutine == null) 
                return;
            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null;
        }

        private void SwitchFollowOn()
        {
            foreach (var f in _follows)
                f.enabled = true;
        }

        private void SwitchFollowOff()
        {
            foreach (var f in _follows)
                f.enabled = false;
        }
    }
}