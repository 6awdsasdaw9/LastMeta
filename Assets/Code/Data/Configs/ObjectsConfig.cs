using Code.Data.GameData;
using Code.Logic.Objects.Spikes;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "ObjectsConfig", menuName = "ScriptableObjects/GameData/ObjectsConfig")]
    public class ObjectsConfig : ScriptableObject
    {
        public SpikeData[] SpikesData;
    }
    
}