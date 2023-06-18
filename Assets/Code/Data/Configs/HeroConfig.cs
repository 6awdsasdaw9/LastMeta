using System;
using Code.Character.Hero;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObjects/GameData/HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        public HeroParams HeroParams;
        public HeroAbilitiesParams AbilitiesParams;
    }

    [Serializable]
    public class HeroAbilitiesParams
    {
        public HeroDashAbility.Data[] DashLevelsData = new HeroDashAbility.Data[3];
    }

 
}