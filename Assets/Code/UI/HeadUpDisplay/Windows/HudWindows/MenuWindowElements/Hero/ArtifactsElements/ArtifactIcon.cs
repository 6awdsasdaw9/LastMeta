using Code.Logic.Items;
using Code.UI.HeadUpDisplay.Elements;
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
            _descriptionPanel?.gameObject.SetActive(true);
        }

        public void DisableIcon()
        {
            _activeImage.SetActive(false);
            _descriptionPanel?.gameObject.SetActive(false);
        }

    }
    
    
}