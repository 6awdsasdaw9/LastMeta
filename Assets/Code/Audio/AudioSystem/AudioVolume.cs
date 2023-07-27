using System;

namespace Code.Audio.AudioSystem
{
    [Serializable]
    public class AudioVolume
    {
        public float Master;
        public float Music;
        public float Effects;
        public bool Mute;
    }
}