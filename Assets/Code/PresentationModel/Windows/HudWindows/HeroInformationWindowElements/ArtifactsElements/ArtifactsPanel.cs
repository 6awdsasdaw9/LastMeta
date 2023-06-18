using UnityEngine;

namespace Code.PresentationModel.Windows.HudWindows.HeroInformationWindowElements
{
    public class ArtifactsPanel: HudElement
    {

        public ArtifactIcon[] ArtifactIcons => _artifactIcons;
        [SerializeField] private ArtifactIcon[] _artifactIcons;
    }
}