using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class DialogueCloud:MonoBehaviour
    {
        [SerializeField] private RectTransform _cloudTransform;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
                                 
        public Vector2 size => new Vector2(_cloudTransform.rect.width, _cloudTransform.rect.height); //270x130
        public void SetRightRotation()
        {
            _image.transform.rotation = Quaternion.Euler(0, 180, 0);
            _text.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        public void SetErrorMessage(string message)
        {
            string colorHex = ColorUtility.ToHtmlStringRGBA(Constants.DarkRedColor);
            _text.SetText($"<color=#{colorHex}>{message}</color>");
            _image.color = Constants.RedColor;
            SetCloudSize(message);
        }

        public void SetText(string message)
        {
            _text.SetText(message);
            _image.color = Color.white;
            SetCloudSize(message);
        }

        private void SetCloudSize(string text)
        {
            /*
            

           //_text.rectTransform.sizeDelta = _text.GetPreferredValues(text);
            _cloudTransform.sizeDelta = sizeDelta;*/
    
            Vector2 contentSize = _text.GetPreferredValues(text);
            _text.rectTransform.sizeDelta = contentSize;

            Debug.Log("Before: contentSize.y " + contentSize.y + " _cloudTransform height" + _cloudTransform.rect.height);
            var padding = 2;
            _cloudTransform.sizeDelta = new Vector2(_cloudTransform.rect.width, contentSize.y /*+ padding*/);

            Debug.Log("After: contentSize.y " + contentSize.y + " _cloudTransform height" + _cloudTransform.rect.height);

            float offsetX = (_cloudTransform.sizeDelta.x - contentSize.x) / 2f - padding / 3f;
            float offsetY = (_cloudTransform.sizeDelta.y - contentSize.y) / 2f + padding ;
            _text.rectTransform.anchoredPosition = new Vector2(offsetX, offsetY);

        }
    }
}