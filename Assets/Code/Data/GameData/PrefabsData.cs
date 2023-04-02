using Code.Character.Hero;
using Code.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/GameSettings/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        [Title("Real Scenes")]
        public HeroMovement realHero;
        public HUD realHUD;
        
        [Space,Title("Game Scenes")]
        public HeroMovement hero;
        public HUD gameHUD;
        
        [Title("FX")] 
        public GameObject fx_PlayerDeath;
    }
}