using Code.Character.Hero;
using Code.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/GameData/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        [Title("Heroes")]
        public HeroMovement realHero;
        public HeroMovement hero;
        
        [Title("FX")] 
        public GameObject fx_PlayerDeath;
        
        [Title("UI")]
        public HUD realHUD;
        public HUD gameHUD;
        public DialogueCloud DialogueCloud;
    }
}
