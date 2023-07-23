using UnityEngine;

namespace Code.UI.HeadUpDisplay.HudElements.Buttons
{
    public class DescriptionButton: MonoBehaviour
    {
        public DescriptionPanel DescriptionPanel => _descriptionPanel;
        [SerializeField] private DescriptionPanel _descriptionPanel;

        private void ShowDescriptionPanel()
        {
            _descriptionPanel.Show();
        }

        private void HideDescriptionPanel()
        {
            _descriptionPanel.Hide();
        }
        
    }
}