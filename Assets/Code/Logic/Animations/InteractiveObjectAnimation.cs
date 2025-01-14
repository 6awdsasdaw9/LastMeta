using System;
using Code.Logic.Objects.Interactive.InteractiveObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class InteractiveObjectAnimation : Interactivity
    {
        [Space,Header("Interactive Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationDuration = 0.5f;
        
        private readonly int Start_t = Animator.StringToHash("Start");
        private readonly int End_t = Animator.StringToHash("End");

        public override void StartInteractive()
        {
            _animator.SetTrigger(Start_t);
            
            OnStartInteractive?.Invoke();
            UpdateProgress();
        }

        public override void StopInteractive()
        {
            _animator.SetTrigger(End_t);
            
            OnStopInteractive?.Invoke();
            UpdateProgress();
        }

        private async void UpdateProgress()
        {
            OnAnimationProcess = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_animationDuration));
            OnAnimationProcess = false;
        }
    }
}