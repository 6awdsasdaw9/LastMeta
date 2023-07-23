using Code.UI.HeadUpDisplay.HudElements;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero.HeroParamsElements
{
    public class HeroParamPanel: HudElement
    {
        [SerializeField] private HeroParamIcon[] _heroParamIcons;
        public HeroParamIcon[] ParamIcons => _heroParamIcons;
    }
}