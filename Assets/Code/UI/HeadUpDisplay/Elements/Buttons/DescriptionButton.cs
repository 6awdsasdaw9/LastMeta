using UnityEngine;

namespace Code.UI.HeadUpDisplay.HudElements.Buttons
{
    public class DescriptionButton: MonoBehaviour
    {
        public DescriptionPanel DescriptionPanel => _descriptionPanel;
        [SerializeField] private DescriptionPanel _descriptionPanel;

        /// <summary>
        /// Animation event
        /// </summary>
        private void ShowDescriptionPanel()
        {
            _descriptionPanel?.Show();
        }

        /// <summary>
        /// Animation event
        /// </summary>
        private void HideDescriptionPanel()
        {
            _descriptionPanel?.Hide();
        }
        
    }
}