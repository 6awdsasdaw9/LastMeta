using Code.Logic.Objects.Items;
using Code.UI.HeadUpDisplay.HudElements;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero.ArtifactsElements
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