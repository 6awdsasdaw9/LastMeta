using Code.Infrastructure.Factories;
using Code.Logic.Objects.Items;
using Code.Logic.Objects.Items.Handlers;
using Code.Services.CurrencyServices;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers.SceneInstallers
{
    public class ItemsInstaller : MonoInstaller
    {
        [SerializeField] private Item _itemPrefab;
        public override void InstallBindings()
        {
            BindItemsPool();
            BindItemFactory();
            BindHanlers();
        }
        
        private void BindItemsPool()
        {
            Container.BindMemoryPool<Item, Item.Pool>()
                .WithInitialSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_itemPrefab)
                .UnderTransformGroup("Pool: Items");
        }

        private void BindItemFactory()
        {
            Container.Bind<ItemFactory>().AsSingle().NonLazy();
        }
        
        private void BindHanlers()
        {
            Container.Bind<ArtefactsHandler>().AsSingle().NonLazy();
            Container.Bind<MoneyHandler>().AsSingle().NonLazy();
        }
    }
}