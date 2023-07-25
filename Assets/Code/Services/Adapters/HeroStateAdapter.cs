using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;

namespace Code.Services.Adapters
{
    public class HeroStateAdapter
    {
        private readonly IHero _hero;
        private readonly EventsFacade _eventsFacade;

        private bool IsWoundedMode;
        
        public HeroStateAdapter(IHero hero, EventsFacade eventsFacade)
        {
            _hero = hero;
            _eventsFacade = eventsFacade;
            
            _hero.Health.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_hero.Stats.IsWounded && !IsWoundedMode)
            {
                _eventsFacade.HeroEvents.HeroWoundEvent();
            }
            else if(IsWoundedMode && !_hero.Stats.IsWounded)
            {
                _eventsFacade.HeroEvents.HeroHealthyEvent();
            }
        }
    }
}