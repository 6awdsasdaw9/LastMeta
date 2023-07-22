using System.Linq;
using Code.Logic.Objects.Items;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsConfig", menuName = "ScriptableObjects/GameData/ItemsConfig")]
public class ItemsConfig : ScriptableObject
{
    public ItemData[] Items;
    public ItemData GetData(ItemType type) => Items.FirstOrDefault(i => i.Type == type);
    
}

namespace Code.Data.Configs
{
}