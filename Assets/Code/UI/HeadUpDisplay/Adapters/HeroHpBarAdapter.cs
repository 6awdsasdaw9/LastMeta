using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Common.Interfaces;
using Code.UI.GameElements;
using Code.UI.GameElements.Adapters;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class HeroHpBarAdapter : HealthBarAdapter
    {
        protected override HpBar hpBar => _hpBar;
        private HpBar _hpBar;
        protected override ICharacterHealth health => _health;
        private ICharacterHealth _health;
        
        [Inject]
        private void Construct(HudFacade hudFacade)
        {
            _hpBar = hudFacade.HeroHpBar;
            _health = GetComponent<IHero>().Health;
        }
    }
}