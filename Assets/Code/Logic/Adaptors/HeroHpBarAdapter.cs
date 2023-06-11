using Code.PresentationModel.HeadUpDisplay;
using Zenject;

namespace Code.Logic.Adaptors
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