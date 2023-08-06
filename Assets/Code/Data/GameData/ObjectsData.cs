using System;
using Code.Logic.Common;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.GameData
{
    #region SPIKES

    public enum SpikeType
    {
        Small,
        Bid,
    }

    [Serializable]
    public class SpikeData
    {
        public SpikeType Type;
        [Range(1, 10)] public float Damage = 1;
         public PushData PushData;
        [GUIColor(0.85f, 0.74f, 1)] public SpikeAudioData AudioData;
    }

    [Serializable]
    public class SpikeAudioData
    {
        public EventReference EnableAudioEvent;
        public EventReference DisableAudioEvent;
        public EventReference CollisionAudioEvent;
    }

    #endregion
}