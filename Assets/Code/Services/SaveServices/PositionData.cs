using System;
using System.Collections.Generic;
using Code.Data.GameData;

namespace Code.Services.SaveServices
{
    [Serializable]
    public class PositionData
    {
        public Dictionary<string, Vector3Data> positionInScene = new();

        public void AddPosition(string scene, Vector3Data position)
        {
            positionInScene.Add(scene,position);
        }
    }
}