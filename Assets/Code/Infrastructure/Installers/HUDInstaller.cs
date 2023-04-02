using Code.Data.GameData;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class HUDInstaller : MonoInstaller
    {
        [Inject] private PrefabsData _prefabsData;
        [SerializeField] private Constants.TypeOfScene _typeOfScene;

        public override void InstallBindings()
        {
            BindHUD();
        }

        private void BindHUD()
        {
            Hud hud = Container.InstantiatePrefabForComponent<Hud>(
                GetHudPrefabs(),
                Vector3.zero,
                Quaternion.identity,
                null);
        }

        private Hud GetHudPrefabs()
        {
            switch (_typeOfScene)
            {
                case Constants.TypeOfScene.Real:
                    return _prefabsData.realHUD;
                case Constants.TypeOfScene.Game:
                default:
                    return _prefabsData.gameHUD;
            }
        }
    }
}