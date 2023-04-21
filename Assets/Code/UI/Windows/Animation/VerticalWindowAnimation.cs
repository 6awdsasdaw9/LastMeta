using System.Collections;
using Code.Data.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows
{
    [RequireComponent(typeof(CanvasGroup))]
    public class VerticalWindowAnimation : WindowAnimation
    {
        [SerializeField] private RectTransform _animatedObject;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private  Vector3 _downPos;
        private  Vector3 _centerPos;
        private  float _timeToHide;
        private  float _timeToShow;

        private const int CYCLE_STEPS = 100;
        private const float ONE_STEP = 1 / (float)CYCLE_STEPS;
        
        
        private Coroutine _animationCoroutine;

        private void KillMePleas()
        {
            _animationCoroutine = StartCoroutine(ShowCoroutine());
        }
        
        
        [Inject]
        private void Construct( GameSettings gameSettings)
        {
            _downPos = gameSettings.InteractiveObjectDownPos;
            _centerPos = gameSettings.InteractiveObjectCenterPos;
            _timeToHide = gameSettings.InteractiveObjectTimeToHide;
            _timeToShow = gameSettings.InteractiveObjectTimeToShow;
        }
        
        public override void PlayShow()
        {
            _animatedObject.anchoredPosition = _downPos; 
            _canvasGroup.alpha = 0;
           
            _animatedObject.gameObject.SetActive(true);
            
           _animationCoroutine = StartCoroutine(ShowCoroutine());
        }

        public override void PlayHide()
        {
            _animatedObject.anchoredPosition = _centerPos; 
            _canvasGroup.alpha = 1;
            
            _animationCoroutine = StartCoroutine(HideCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            IsPlay = true;
            
            _animatedObject.DOAnchorPos(_centerPos, _timeToShow);
            
            var speed = ONE_STEP / (1 / _timeToShow);

            for (int i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha += ONE_STEP;
                yield return new WaitForSeconds(speed);
            }

            IsPlay = false;
        }

        private IEnumerator HideCoroutine()
        {
            IsPlay = true;
            _animatedObject.DOAnchorPos(_downPos, _timeToHide);
            
            var speed = ONE_STEP / (1 / _timeToHide);
            for (int i = 0; i < CYCLE_STEPS; i++)
            {
                _canvasGroup.alpha -= ONE_STEP;
                yield return new WaitForSeconds(speed);
            }
            
            IsPlay = false;
            _animatedObject.gameObject.SetActive(false);
        }

    }
}