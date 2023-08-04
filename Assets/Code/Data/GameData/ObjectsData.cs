using System;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.GameData
{
    #region SPIKES

    public enum SpikeType
    {
        BidAngle,
        BigForward,
        SmallAngle,
        SmallForward
    }

    [Serializable]
    public class SpikeData
    {
        public SpikeType Type;
        [Range(1, 10)] public float Damage = 1;
        [Range(0, 5)] public float PushForce = 0.5f;
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