using Zenject;

namespace Code.UI
{
    public class HeroActorUI : ActorUI
    {
        [Inject]
        private void Construct(Hud hud)
        {
            _hpBar = hud.HeroHpBar;
        }
    }
}