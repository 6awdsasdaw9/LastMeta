using Code.Data.GameData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs.HeroConfigs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObjects/GameData/HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        [GUIColor(0.2f,0.5f,0.8f)]
        public HeroParams HeroParams;
        [GUIColor(0.2f,0.8f,0.5f)]
        public HeroAbilitiesParams AbilitiesParams;
        [GUIColor(0.8f,0.5f,0.2f)]
        public HeroUpgradesParams UpgradesParams;
        [GUIColor(0.85f, 0.74f, 1)]
        public HeroAudioPath AudioPath;
    }
}