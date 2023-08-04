using System;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Objects.Spikes
{
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
        public RuntimeAnimatorController AnimatorController;
        public float Damage = 1;

        [GUIColor(0.85f, 0.74f, 1)] 
        private readonly SpikeAudioData _spikeAudioData = new SpikeAudioData();
    }

    [Serializable]
    public class SpikeAudioData
    {
        public EventReference EnableAudioEvent;
        public EventReference DisableAudioEvent;
        public EventReference CollisionAudioEvent;
    }
}