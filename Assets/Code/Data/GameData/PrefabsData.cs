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
        public GameObject realHud;
        
        [Space,Title("Game Scenes")]
        public HeroMovement hero;
        public Hud hud;
        
        [Title("FX")] 
        public GameObject fx_PlayerDeath;
    }
}