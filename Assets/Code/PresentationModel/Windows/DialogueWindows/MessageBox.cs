using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel.Windows.DialogueWindows
{
    public class MessageBox: MonoBehaviour
    {
        [SerializeField] private RectTransform _boxTransform;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _messageText;
        public Vector2 size => new(_boxTransform.rect.width, _boxTransform.rect.height); //270x130
                                 
        public void SetRightRotation()
        {
            _backgroundImage.transform.rotation = Quaternion.Euler(0, 180, 0);
            _messageText.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        public void SetErrorMessage(string message)
        {
            string colorHex = ColorUtility.ToHtmlStringRGBA(Constants.DarkRedColor);
            _messageText.SetText($"<color=#{colorHex}>{message}</color>");
            _backgroundImage.color = Constants.RedColor;
            SetCloudSize(message);
        }

        
        public void SetText(string message )
        {
            _messageText.SetText(message);
        }

        public void SetColor(Color imageColor)
        {
            _backgroundImage.color = imageColor;
        }
        private void SetCloudSize(string text)
        {
            Vector2 contentSize = _messageText.GetPreferredValues(text);
            _messageText.rectTransform.sizeDelta = contentSize;

            Debug.Log("Before: contentSize.y " + contentSize.y + " _cloudTransform height" + _boxTransform.rect.height);
            var padding = 2;
            _boxTransform.sizeDelta = new Vector2(_boxTransform.rect.width, contentSize.y /*+ padding*/);

            Debug.Log("After: contentSize.y " + contentSize.y + " _cloudTransform height" + _boxTransform.rect.height);

            float offsetX = (_boxTransform.sizeDelta.x - contentSize.x) / 2f - padding / 3f;
            float offsetY = (_boxTransform.sizeDelta.y - contentSize.y) / 2f + padding ;
            _messageText.rectTransform.anchoredPosition = new Vector2(offsetX, offsetY);

        }
    }
}