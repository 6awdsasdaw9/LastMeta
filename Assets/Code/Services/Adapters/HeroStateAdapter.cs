using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
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
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            if (_hero.Stats.IsWounded && !IsWoundedMode)
            {
                IsWoundedMode = true;
                _eventsFacade.HeroEvents.HeroWoundEvent();
            }
            else if(!_hero.Stats.IsWounded && IsWoundedMode)
            {
                IsWoundedMode = false;
                _eventsFacade.HeroEvents.HeroHealthyEvent();
            }
        }
    }
}