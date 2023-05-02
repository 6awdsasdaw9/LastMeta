using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects
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
            
            OnEndInteractive?.Invoke();
            UpdateProgress();
        }

        private async void UpdateProgress()
        {
            OnProcess = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_animationDuration));
            OnProcess = false;
        }
    }
}