using Code.Logic.Artifacts.Artifacts;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "ScriptableObjects/GameData/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        public Artifact[] Artifacts;
    }
}