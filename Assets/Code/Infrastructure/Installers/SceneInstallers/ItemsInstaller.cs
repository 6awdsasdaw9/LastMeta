using Code.Infrastructure.Factories;
using Code.Logic.Artifacts;
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
        }
        
        private void BindItemsPool()
        {
            Container.BindMemoryPool<Item, Item.Pool>()
                .WithInitialSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_itemPrefab)
                .UnderTransformGroup("Items");
        }

        private void BindItemFactory()
        {
            Container.Bind<ItemFactory>().AsSingle().NonLazy();
        }
    }
}