using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class InteractiveHudImage : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private HUD _hud;
        [SerializeField] private Image _interactiveImage;
      
        private bool _isMove;

        private readonly Vector3 downPos = Vector3.down * 600;
        private readonly Vector3 centerPos = Vector3.zero;
        private const float timeToHide = 0.35f;
        private const float timeToShow = 1.5f;
        
        public void ShowInteractiveImage(Sprite sprite)
        {
            if(_isMove)
                return;
            
            _interactiveImage.sprite = sprite;
            _interactiveImage.rectTransform.anchoredPosition = downPos; 
            _interactiveImage.gameObject.SetActive(true);
            
            _hud.OnUIWindowShown?.Invoke();
            
            StartCoroutine(ShowCoroutine());
        }

        public  void HideInteractiveImage()
        {
            if(_isMove)
                return;
            
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            _isMove = true;
            _interactiveImage.rectTransform.DOAnchorPos(centerPos, timeToShow);
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            _isMove = false;
        }

        private IEnumerator HideCoroutine()
        {
            _isMove = true;
            _interactiveImage.rectTransform.DOAnchorPos(downPos, timeToHide);
            
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.03f;
                yield return new WaitForSeconds(0.01f);
            }
            
            _hud.OnUIWindowHidden?.Invoke();
            
            yield return new WaitForSeconds(timeToHide);
            _interactiveImage.rectTransform.DOAnchorPos(downPos, 0);
            _isMove = false;
            _interactiveImage.gameObject.SetActive(false);
        }
    }
}