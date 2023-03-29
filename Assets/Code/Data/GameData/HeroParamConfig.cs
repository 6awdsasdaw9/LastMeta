using System;

namespace Code.Data.GameData
{
    public enum ParamType
    {
        Speed,
        JumpHeight
    }

    [Serializable]
    public class HeroParamConfig
    {
        public int Lvl;
        public ParamType Param;
        public float Value;
    }
}