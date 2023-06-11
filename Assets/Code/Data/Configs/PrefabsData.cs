using Code.Character.Hero;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/GameData/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        [Title("Heroes")]
        public Hero RealHeroPrefab;
        public Hero HeroPrefab;
        
        [Title("FX")] 
        public GameObject VFX_PlayerDeath;
        
  
    }
}
