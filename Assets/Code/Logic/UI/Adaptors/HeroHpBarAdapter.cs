using Code.UI.HeadUpDisplay;
using Zenject;

namespace Code.UI.Adaptors
{
    public class HeroHpBarAdapter : HpBarAdapter
    {
        [Inject]
        private void Construct(HUD hud)
        {
            _hpBar = hud.HeroHpBar;
        }
    }
}