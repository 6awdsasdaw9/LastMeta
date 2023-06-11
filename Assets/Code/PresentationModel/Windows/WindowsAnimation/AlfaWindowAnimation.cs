using System;
using Code.Data.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.PresentationModel.Windows.WindowsAnimation
{
    public class AlfaWindowAnimation : WindowAnimation
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private  float _timeToHide;
        private  float _timeToShow;

        private const int CYCLE_STEPS = 100;
        private const float ONE_STEP = 1 / (float)CYCLE_STEPS;

        
        [Inject]
        private void Construct( HudSettings hudSettings)
        {
            _timeToHide = hudSettings.InteractiveUIParams.InteractiveObjectTimeToHide;
            _timeToShow = hudSettings.InteractiveUIParams.InteractiveObjectTimeToShow;
        }
        
        public override void PlayShow(Action WindowShowed = null)
        {
            ShowAnimation(WindowShowed).Forget();
        }

        public override void PlayHide(Action WindowHidden = null)
        {
            HideAnimation(WindowHidden).Forget();
        }
        
        private async UniTaskVoid ShowAnimation(Action WindowShowed)
        {
            _canvasGroup.gameObject.SetActive(true);
            IsPlay = true;

            _canvasGroup.alpha = 0;
            var speed = ONE_STEP / (1 / _timeToShow);
            
            for (var i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha += ONE_STEP;
                await UniTask.Delay(TimeSpan.FromSeconds(speed));
            }

            WindowShowed?.Invoke();
            IsPlay = false;
        }
        
        private async UniTaskVoid HideAnimation(Action WindowHidden)
        {
            IsPlay = true;
            
            var speed = ONE_STEP / (1 / _timeToHide);
            _canvasGroup.alpha = 1;
            
            for (var i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha -= ONE_STEP;
                await UniTask.Delay(TimeSpan.FromSeconds(speed));
            }

            IsPlay = false;
            WindowHidden?.Invoke();
            _canvasGroup.gameObject.SetActive(false);
        }
    }
}
