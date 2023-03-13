using Code.Character.Hero;
using Code.UI;
using UnityEngine;

namespace Code.Data.GameData
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "ScriptableObjects/GameSettings/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        public HeroMovement hero;
        public Hud hud;
    }
}