using System.Collections;
using Code.Data.GameData;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InteractiveWindowAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _animatedObject;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private  Vector3 downPos;
        private  Vector3 centerPos;
        private  float timeToHide;
        private  float timeToShow;

        private const int cycleSteps = 100;
        private const float oneStep = 1 / (float)cycleSteps; 
        public bool isMove { get; private set; }

        [Inject]
        private void Construct( SettingsData settingsData)
        {
            downPos = settingsData.InteractiveObjectDownPos;
            centerPos = settingsData.InteractiveObjectCenterPos;
            timeToHide = settingsData.InteractiveObjectTimeToHide;
            timeToShow = settingsData.InteractiveObjectTimeToShow;
        }
        
        public void PlayShow()
        {
            _animatedObject.anchoredPosition = downPos; 
            _canvasGroup.alpha = 0;
           
            _animatedObject.gameObject.SetActive(true);
            
            StartCoroutine(ShowCoroutine());
        }

        public  void PlayHide()
        {
            _animatedObject.anchoredPosition = centerPos; 
            _canvasGroup.alpha = 1;
            
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            isMove = true;
            
            _animatedObject.DOAnchorPos(centerPos, timeToShow);
            
            var speed = oneStep / (1 / timeToShow);

            for (int i = 0; i < cycleSteps; i++)
            {
                _canvasGroup.alpha += oneStep;
                yield return new WaitForSeconds(speed);
            }

            isMove = false;
        }

        private IEnumerator HideCoroutine()
        {
            isMove = true;
            _animatedObject.DOAnchorPos(downPos, timeToHide);
            
            var speed = oneStep / (1 / timeToHide);
            for (int i = 0; i < cycleSteps; i++)
            {
                _canvasGroup.alpha -= oneStep;
                yield return new WaitForSeconds(speed);
            }
            
            isMove = false;
            _animatedObject.gameObject.SetActive(false);
        }

    }
}