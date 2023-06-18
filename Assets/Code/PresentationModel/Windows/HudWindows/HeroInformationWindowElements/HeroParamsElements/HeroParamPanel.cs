using UnityEngine;

namespace Code.PresentationModel.Windows.HudWindows.HeroInformationWindowElements
{
    public class HeroParamPanel: HudElement
    {
        [SerializeField] private HeroParamIcon[] _heroParamIcons;
        public HeroParamIcon[] ParamIcons => _heroParamIcons;
    }
}