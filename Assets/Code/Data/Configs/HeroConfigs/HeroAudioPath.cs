using System;
using FMODUnity;

namespace Code.Data.Configs
{
    [Serializable]
    public class HeroAudioPath
    {
        public  EventReference Step;
        public  EventReference SoftStep;
        public  EventReference TakeDamage;
        public  EventReference Dash;
        public  EventReference Jump;
        public  EventReference OnLoad;
        public  EventReference Punch;
        public  EventReference Shoot;
        public  EventReference Stunned;
        public  EventReference WaterDeath;
    }
}