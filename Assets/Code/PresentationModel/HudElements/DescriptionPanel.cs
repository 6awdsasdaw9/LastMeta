using Code.Logic.LanguageLocalization;
using Code.PresentationModel.Windows.WindowsAnimation;
using TMPro;
using UnityEngine;

namespace Code.PresentationModel.Buttons
{
    public class DescriptionPanel : HudElement, ILocalizationTitle, ILocalizationDescription
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

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
    }
}