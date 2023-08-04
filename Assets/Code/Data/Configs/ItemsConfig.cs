using System.Linq;
using Code.Logic.Items;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "ScriptableObjects/GameData/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        public ItemData[] Items;
        public ItemData GetData(ItemType type) => Items.FirstOrDefault(i => i.Type == type);
    
    }
}