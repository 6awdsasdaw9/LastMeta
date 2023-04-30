using Code.Data.GameData;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public Constants.Scenes initialScene;
        public HeroConfig heroConfig;
    }

}