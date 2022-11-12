using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.Infrastructure.Facroty
{
    //Creates and instantiates objects
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero(GameObject at)
        {
            return _assets.Instantiate(AssetPath.HeroPath, at: at.transform.position);
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPath.HudPath);
        }

    }
}