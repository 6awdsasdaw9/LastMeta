using Zenject;

namespace Code.UI.Actors
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