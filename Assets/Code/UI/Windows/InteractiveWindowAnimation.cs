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

            var time = 0f;
            float step = timeToShow * 0.1f;
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += step;
                time += 0.01f;
                Debug.Log(time);
                yield return new WaitForSeconds(step);
            }
            isMove = false;
        }

        private IEnumerator HideCoroutine()
        {
     
            isMove = true;
            _animatedObject.DOAnchorPos(downPos, timeToHide);
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(timeToHide);
            _animatedObject.DOAnchorPos(downPos, 0);
            isMove = false;
            _animatedObject.gameObject.SetActive(false);
        }

    }
}