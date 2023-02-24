using System.Collections;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver triggerObserver;
        public Follow follow;

        public float cooldown;
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
                _hasAggroTarget = false;
            }
        }

        private void TriggerEnter(Collider obj)
        {
            if(!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOn();
            }
        }


        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            SwitchFollowOff();
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void SwitchFollowOn() =>
            follow.enabled = true;

        private void SwitchFollowOff() =>
            follow.enabled = false;
    }
}