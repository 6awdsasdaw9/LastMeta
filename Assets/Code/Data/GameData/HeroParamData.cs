using System;

namespace Code.Data.GameData
{
    public enum ParamType
    {
        Speed,
        JumpHeight
    }

    [Serializable]
    public class HeroParamData
    {
        public int Lvl;
        public ParamType Param;
        public float Value;
    }
}