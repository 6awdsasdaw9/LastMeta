using System;
using Code.Data.Configs;
using Code.Debugers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.UI.Windows.Animation
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
            Log.ColorLog($"{gameObject.name}  play show");
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