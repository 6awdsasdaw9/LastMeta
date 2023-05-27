using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "SpawnPointsConfig", menuName = "ScriptableObjects/GameData/SpawnPointsConfig")]
    public class SpawnPointsConfig : ScriptableObject
    {
        public List<ScenesSpawnPointsData> SceneSpawnPoints;
    }

    [Serializable]
    public class ScenesSpawnPointsData
    {
        public Constants.Scenes Scene;
        public List<PointData> Points;
    }

    [Serializable]
    public class PointData
    {
        public int ID;
        public Vector3 Position;
    }
}