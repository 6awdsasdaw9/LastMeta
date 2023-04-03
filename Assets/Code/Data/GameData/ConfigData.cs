using Code.Data.States;
using UnityEngine;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "ScriptableObjects/GameData/ConfigData")]
    public class ConfigData : ScriptableObject
    {
        public Constants.Scenes initialScene;
        public HeroConfig heroConfig;
    }
}