using System;
using Code.Data.Configs;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Artifacts;
using Code.Logic.Missile;

namespace Code.Infrastructure.Factories
{
    public class ItemFactory
    {
        private readonly Item.Pool _itemPool;
        private readonly ItemsConfig _itemsConfig;
        private readonly EventsFacade _eventsFacade;

        public ItemFactory(Item.Pool itemPool, ItemsConfig itemsConfig, EventsFacade eventsFacade)
        {
            _itemPool = itemPool;
            _itemsConfig = itemsConfig;
            _eventsFacade = eventsFacade;
            _eventsFacade.ItemEvents.OnPickUpItem += DeSpawnItem;
        }

        public Item SpawnItem(ItemType itemType)
        {
            var data = _itemsConfig.GetData(itemType);
            if (data == null) return null;

            var item = _itemPool.Spawn(data, _eventsFacade);
            item.SetBehavior(GetItemBehaviour(itemType));
            return item;
        }

        private void DeSpawnItem(Item item)
        {
            _itemPool.Despawn(item);
        }

        private ItemBehaviour GetItemBehaviour(ItemType itemType)
        {
            switch (itemType)
            {
                default:
                case ItemType.Money:
                case ItemType.HealthBonus:
                case ItemType.SpeedBonus:
                case ItemType.LeftSock:
                case ItemType.Seed:
                case ItemType.RightSock:
                case ItemType.Key:
                case ItemType.Glove:
                case ItemType.Gun:
                case ItemType.Substance:
                    return null;
            }
        }
    }
}