using Code.Logic.Artifacts;
using Code.PresentationModel.Buttons;
using UnityEngine;

namespace Code.PresentationModel.Windows.HudWindows.HeroInformationWindowElements
{
    public class ArtifactIcon: HudElement
    {
        public ItemType Type;
        [SerializeField] private GameObject _activeImage;
        
        public DescriptionPanel DescriptionPanel => _descriptionPanel;
        [SerializeField] private DescriptionPanel _descriptionPanel;

        public void EnableIcon()
        {
            _activeImage.SetActive(true);
        }

        public void SetDescription(string title)
        {
            
        }

    }
    
    
}