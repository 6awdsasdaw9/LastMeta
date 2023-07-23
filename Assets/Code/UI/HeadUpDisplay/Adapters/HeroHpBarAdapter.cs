using Code.UI.GameElements.Adapters;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroHpBarAdapter : HpBarAdapter
    {
        [Inject]
        private void Construct(HudFacade hudFacade)
        {
            _hpBar = hudFacade.HeroHpBar;
        }
    }
}