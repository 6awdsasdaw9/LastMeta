using System;
using Sirenix.OdinInspector;

namespace Code.Data.Configs.HeroConfigs
{
    public enum HeroUpgradeParamType
    {
        Health,
        Damage,
        Speed,
        Jump,
    }
    
    [Serializable]
    public class HeroUpgradesParams
    {
        //[GUIColor()]
        public HealthUpgradesData HealthUpgradesData;
        public AttackUpgradesData AttackUpgradesData;
        public SpeedUpgradesData SpeedUpgradesData;
        public JumpUpgradesData JumpUpgradesData;
   
        public int GetMaxLevel<T>() where T : HeroUpgradeData
        {
            if (HealthUpgradesData is T health) return HealthUpgradesData.Values.Length - 1;
            if (AttackUpgradesData is T attack) return AttackUpgradesData.Values.Length - 1;
            if (SpeedUpgradesData is T speed) return SpeedUpgradesData.Values.Length - 1;
            if (JumpUpgradesData is T jump) return JumpUpgradesData.Values.Length - 1;
            return -1;
        }

        public float GetValueByLevel<T>(int level) where T : HeroUpgradeData
        {
            if (level < 0)
            {
                return 0;
            }

            if (level < GetMaxLevel<HealthUpgradesData>())
                if (HealthUpgradesData is T health)
                    return HealthUpgradesData.Values[level];
            if (level < GetMaxLevel<AttackUpgradesData>())
                if (AttackUpgradesData is T attack)
                    return AttackUpgradesData.Values[level];
            if (level < GetMaxLevel<SpeedUpgradesData>())
                if (SpeedUpgradesData is T speed)
                    return SpeedUpgradesData.Values[level];
            if (level < GetMaxLevel<JumpUpgradesData>())
                if (JumpUpgradesData is T jump)
                    return JumpUpgradesData.Values[level];

            return 0;
        }
   
    }
    
    
    [Serializable]
    public class JumpUpgradesData : HeroUpgradeData
    {
       [ShowInInspector,ReadOnly] public override HeroUpgradeParamType Type => HeroUpgradeParamType.Jump;
    }
    [Serializable]
    public class SpeedUpgradesData : HeroUpgradeData
    {
       [ShowInInspector,ReadOnly] public override HeroUpgradeParamType Type => HeroUpgradeParamType.Speed;
    }
    
    [Serializable]
    public class AttackUpgradesData : HeroUpgradeData
    {
       [ShowInInspector,ReadOnly] public override HeroUpgradeParamType Type => HeroUpgradeParamType.Damage;
    }
    
    [Serializable]
    public class HealthUpgradesData : HeroUpgradeData
    {
       [ShowInInspector,ReadOnly] public override HeroUpgradeParamType Type => HeroUpgradeParamType.Health;
    }
    
    public abstract class HeroUpgradeData
    {
        public abstract HeroUpgradeParamType Type { get; }
        public float[] Values = new float[3];
    }

  
}