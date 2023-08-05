using Code.Character.Hero;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "AssetsConfig", menuName = "ScriptableObjects/GameData/AssetsConfig")]
    public class AssetsConfig : ScriptableObject
    {
        [Title("Heroes")]
        public Hero RealHeroPrefab;
        public Hero HeroPrefab;
        
        [Title("FX")] 
        public GameObject VFX_PlayerDeath;

        [Title("Graphics Material")] 
        public Material CharacterMaterial;
        public Material GlitchMaterial;
        
        [Title("Physics Material")] 
        public PhysicMaterial NoFrictionMaterial;
        public PhysicMaterial FrictionMaterial;
    }
    
}
