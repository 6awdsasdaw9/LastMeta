using System;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        public Action OnEnter;

        public void EndEnterAnimationEvent()
        {
            OnEnter?.Invoke();
        }
    }
}