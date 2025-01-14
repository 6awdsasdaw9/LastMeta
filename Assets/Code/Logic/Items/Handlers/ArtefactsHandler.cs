using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Services.EventsSubscribes;

namespace Code.Logic.Items.Handlers
{
    public class ArtefactsHandler : IEventsSubscriber
    {
        private readonly EventsFacade _eventsFacade;
        private readonly IHero _hero;
        
        public ArtefactsHandler(EventsFacade eventsFacade, IHero hero, EventSubsribersStorage eventSubsribersStorage)
        {
            _eventsFacade = eventsFacade;
            _hero = hero;
            eventSubsribersStorage.Add(this);
            SubscribeToEvents(true);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.ItemEvents.OnPickUpItem += OnPickUpItem;
            }
            else
            {
                _eventsFacade.ItemEvents.OnPickUpItem -= OnPickUpItem;
            }
        }

        private void OnPickUpItem(ItemData itemData)
        {
            switch (itemData.Type)
            {
                case ItemType.RightSock:
                    _hero.Ability.LevelUpDash();
                    break;
                case ItemType.LeftSock:
                    _hero.Ability.LevelUpSuperJump();
                    break;
                case ItemType.Glove:
                    _hero.Ability.LevelUpHandAttack();
                    break;
                case ItemType.Gun:
                    _hero.Ability.LevelUpGunAttack();
                    break;
                case ItemType.Substance:
                    break;
            }
        }
    }
}