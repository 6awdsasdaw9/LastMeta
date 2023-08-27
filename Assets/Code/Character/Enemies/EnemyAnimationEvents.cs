using System;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        public Action OnEnter;
        public Action OnAttack;
        public Action OnAttackEnded;
        public Action OnRangeAttack;
        public Action OnRangeAttackEnded;

        //--------------------------------------------------------------------------------------------------------------

        private void AnimationEvent_EndEnter() => OnEnter?.Invoke();
        //--------------------------------------------------------------------------------------------------------------

        private void AnimationEvent_Attack() => OnAttack?.Invoke();

        private void AnimationEvent_AttackEnded() => OnAttackEnded?.Invoke();
        //--------------------------------------------------------------------------------------------------------------

        private void AnimationEvent_RangeAttack() => OnRangeAttack?.Invoke();

        private void AnimationEvent_RangeAttackEnded() => OnRangeAttackEnded?.Invoke();
        //--------------------------------------------------------------------------------------------------------------
    }
}