using Code.Data.Stats;
using UnityEngine;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameSettings/Game Config")]
    public class GameConfig : ScriptableObject
    {
        public PlayerConfig playerConfig;
    }
}