using System;
using Code.Data.Configs;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Missile;
using Code.Logic.Objects.Items;
using Code.Logic.Objects.Items.ItemBehavious;

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
        }

        public Item SpawnItem(ItemType itemType)
        {
            var data = _itemsConfig.GetData(itemType);
            if (data == null) return null;

            var item = _itemPool.Spawn(data, _eventsFacade);
            var itemBehavior = GetItemBehaviour(item);
            
            item.SetBehavior(itemBehavior);
            return item;
        }

        public void DeSpawnItem(Item item)
        {
            _itemPool.Despawn(item);
        }

        private ItemBehaviour GetItemBehaviour(Item item)
        {
            switch (item.Data.Type)
            {
                default:
                case ItemType.Money:
                    return new ItemMoneyBehavior(item);
                case ItemType.HealthBonus:
                case ItemType.SpeedBonus:
                case ItemType.LeftSock:
                case ItemType.Seed:
                case ItemType.RightSock:
                    return new ItemMoneyBehavior(item);
                case ItemType.Key:
                case ItemType.Glove:
                case ItemType.Gun:
                case ItemType.Substance:
                    return null;
            }
        }
    }
}