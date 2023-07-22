using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Services;

namespace Code.Logic.Objects.Items.Handlers
{
    public class ArtefactsHandler : IEventSubscriber
    {
        private readonly EventsFacade _eventsFacade;
        private readonly IHero _hero;

        public ArtefactsHandler(EventsFacade eventsFacade, IHero hero)
        {
            _eventsFacade = eventsFacade;
            _hero = hero;
            SubscribeToEvent(true);
        }

        public void SubscribeToEvent(bool flag)
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