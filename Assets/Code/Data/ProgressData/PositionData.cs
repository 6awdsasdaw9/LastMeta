using System;
using System.Collections.Generic;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Data.ProgressData
{
    [Serializable]
    public class PositionData
    {
        //public string scene;
        public Vector3Data position;
        public Dictionary<string, Vector3Data> positionInScene = new();

      

        public void AddPosition(string scene, Vector3Data position)
        {
            positionInScene.Add(scene,position);
        }
    }
}