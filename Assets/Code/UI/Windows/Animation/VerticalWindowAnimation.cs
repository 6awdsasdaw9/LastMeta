using System;
using Code.Data.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
    public class VerticalWindowAnimation : WindowAnimation
    {
        [SerializeField] private RectTransform _body;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private  Vector3 _downPos;
        private  Vector3 _centerPos;
        private  float _timeToHide;
        private  float _timeToShow;

        private const int CYCLE_STEPS = 100;
        private const float ONE_STEP = 1 / (float)CYCLE_STEPS;
        
        [Inject]
        private void Construct( GameSettings gameSettings)
        {
            _downPos = gameSettings.InteractiveObjectDownPos;
            _centerPos = gameSettings.InteractiveObjectCenterPos;
            _timeToHide = gameSettings.InteractiveObjectTimeToHide;
            _timeToShow = gameSettings.InteractiveObjectTimeToShow;
        }
        
        public override void PlayShow(Action WindowShowed)
        {
            _body.anchoredPosition = _downPos; 
            _canvasGroup.alpha = 0;
           
            _body.gameObject.SetActive(true);
            
           ShowAnimation(WindowShowed);
        }

        public override void PlayHide(Action WindowHidden)
        {
            _body.anchoredPosition = _centerPos; 
            _canvasGroup.alpha = 1;

            HideAnimation(WindowHidden);
        }

        private async void ShowAnimation(Action WindowHidden)
        {
            IsPlay = true;
            _body.DOAnchorPos(_centerPos, _timeToShow);
            
            var speed = ONE_STEP / (1 / _timeToShow);

            for (int i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha += ONE_STEP;
                await UniTask.Delay(TimeSpan.FromSeconds(speed));
            }

            WindowHidden?.Invoke();
            IsPlay = false;
        }

        private async void HideAnimation(Action WindowHidden)
        {
            IsPlay = true;
            _body.DOAnchorPos(_downPos, _timeToHide);
            
            var speed = ONE_STEP / (1 / _timeToHide);
            for (int i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha -= ONE_STEP;
                await UniTask.Delay(TimeSpan.FromSeconds(speed));
            }
            
            IsPlay = false;
            WindowHidden?.Invoke();
            _body.gameObject.SetActive(false);
        }
    }
}