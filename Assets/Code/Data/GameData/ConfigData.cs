using Code.Data.Stats;
using UnityEngine;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "ConfigData", menuName = "ScriptableObjects/GameSettings/ConfigData")]
    public class ConfigData : ScriptableObject
    {
        public PlayerConfig playerConfig;
    }
}