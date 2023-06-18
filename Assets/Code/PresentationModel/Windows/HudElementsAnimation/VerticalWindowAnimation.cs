using System;
using Code.Data.Configs;
using Code.Debugers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.PresentationModel.Windows.WindowsAnimation
{
    public class VerticalWindowAnimation : WindowAnimation
    {
        [SerializeField] private RectTransform _body;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Vector3 _downPos;
        private Vector3 _centerPos;
        private float _timeToHide;
        private float _timeToShow;


        [Inject]
        private void Construct(HudSettings hudSettings)
        {
            _downPos = hudSettings.InteractiveUIParams.InteractiveObjectDownPos;
            _centerPos =  hudSettings.InteractiveUIParams.InteractiveObjectCenterPos;
            _timeToHide =  hudSettings.InteractiveUIParams.InteractiveObjectTimeToHide;
            _timeToShow =  hudSettings.InteractiveUIParams.InteractiveObjectTimeToShow;
        }

        public override void PlayShow(Action WindowShowed)
        {
            Logg.ColorLog($"1. VerticalWindowAnimation: {gameObject.name} play show", ColorType.Lightblue);
            _body.anchoredPosition = _downPos;
            _canvasGroup.alpha = 0;

            _body.gameObject.SetActive(true);

            IsPlay = true;

            _body.DOAnchorPos(_centerPos, _timeToShow)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

            _canvasGroup.DOFade(1, _timeToShow)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy)
                .OnComplete(() =>
                {
                    WindowShowed?.Invoke();
                    IsPlay = false;
                });
        }

        public override void PlayHide(Action WindowHidden)
        {
            Logg.ColorLog($"2. VerticalWindowAnimation: {gameObject.name} play hide", ColorType.Lightblue);
            _body.anchoredPosition = _centerPos;
            _canvasGroup.alpha = 1;

            IsPlay = true;

            _body.DOAnchorPos(_downPos, _timeToHide)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

            _canvasGroup.DOFade(0, _timeToHide)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy)
                .OnComplete(() =>
                {
                    IsPlay = false;
                    WindowHidden?.Invoke();
                    _body.gameObject.SetActive(false);
                });
        }
    }
}