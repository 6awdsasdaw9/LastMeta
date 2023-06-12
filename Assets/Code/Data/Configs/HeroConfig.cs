using Code.Data.GameData;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "ScriptableObjects/GameData/HeroConfig")]
    public class HeroConfig : ScriptableObject
    {
        public HeroParams HeroParams;
    }

}