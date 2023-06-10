using Code.UI.HeadUpDisplay;
using Zenject;

namespace Code.UI.Adaptors
{
    public class HeroHpBarAdapter : HpBarAdapter
    {
        [Inject]
        private void Construct(Hud hud)
        {
            _hpBar = hud.HeroHpBar;
        }
    }
}