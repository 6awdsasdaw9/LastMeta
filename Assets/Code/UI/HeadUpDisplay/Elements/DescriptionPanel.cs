using Code.Services.LanguageLocalization;
using Code.UI.HeadUpDisplay.Windows.HudElementsAnimation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Elements
{
    public class DescriptionPanel : HudElement, ILocalizationTitle, ILocalizationDescription
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private RectTransform _backgroundImage;
        
        [SerializeField] private AlfaHudElementAnimation _animation;

        private void Awake()
        {
            base.Hide();
        }

        public override void Show()
        {
            base.Show();
            _animation.PlayShow();
        }

        public override void Hide()
        {
            _animation.PlayHide(base.Hide);
        }


        public void SetTitle(string title)
        {
            _titleText.SetText(title);
        }

        public void SetDescription(string description)
        {
            _descriptionText.SetText(description);
        }

        public void SetLevel(string level)
        {
            _levelText.SetText(level);
        }

        [Button]
        private void FlipDestionPanel()
        {
            _backgroundImage.transform.localScale = new Vector3(-_backgroundImage.transform.localScale.x, 1, 1);
        }
    }
}