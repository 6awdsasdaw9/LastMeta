using System.Collections.Generic;
using Code.Character.Hero;
using Code.Data.GameData;
using Code.Logic.Objects.Spikes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "ObjectsConfig", menuName = "ScriptableObjects/GameData/ObjectsConfig")]
    public class ObjectsConfig : ScriptableObject
    {
        [GUIColor(0.8f,0.1f,0.4f)]
        public SpikeData[] SpikesData;
    }
    
}