using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Data.Configs
{
    [CreateAssetMenu(fileName = "SpawnPointsData", menuName = "ScriptableObjects/GameData/SpawnPoints")]
    public class SpawnPointsData : ScriptableObject
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