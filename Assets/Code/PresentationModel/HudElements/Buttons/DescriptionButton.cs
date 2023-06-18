using UnityEngine;

namespace Code.PresentationModel.Buttons
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