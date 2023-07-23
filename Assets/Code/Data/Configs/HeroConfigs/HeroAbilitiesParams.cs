using System;
using Code.Character.Hero.Abilities;
using Code.Data.GameData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Data.Configs.HeroConfigs
{
    [Serializable]
    public class HeroAbilitiesParams
    {
        public bool IsDashMax(int level) => level >= DashLevelsData.Length - 1;
        [GUIColor(0.2f,0.5f,0.8f)]
        public HeroDashAbility.Data[] DashLevelsData = new HeroDashAbility.Data[3];
        public bool IsHandMax(int level) => level >= HandAttackLevelsData.Length - 1;
        [GUIColor(0.2f,0.6f,0.7f)]
        public HeroHandAttackAbility.Data[] HandAttackLevelsData = new HeroHandAttackAbility.Data[3];
        public bool IsGunMax(int level) => level >= GunAttackLevelData.Length - 1;
        [GUIColor(0.2f,0.7f,0.6f)]
        public ShootingParams[] GunAttackLevelData = new ShootingParams[3];
        private bool IsSuperJumpMax(int level) => level >= SuperJumpData.Length - 1;
        [GUIColor(0.2f,0.8f,0.5f)]
        public HeroSuperJumpAbility.Data[] SuperJumpData = new HeroSuperJumpAbility.Data[3];


        public int GetMaxLevel<T>() where T : AbilitySettings
        {
            if (DashLevelsData[0] is T dash) return DashLevelsData.Length - 1;
            if (HandAttackLevelsData[0] is T handAttack) return HandAttackLevelsData.Length - 1;
            if (GunAttackLevelData[0] is T gunAttack) return GunAttackLevelData.Length - 1;
            if (SuperJumpData[0] is T superJump) return SuperJumpData.Length - 1;
            return -1;
        }

        public T GetSettingsByLevel<T>(int level) where T : AbilitySettings
        {
            if (level < 0)
            {
                Debug.LogError("Upgrade level can't be less than 0");
                return null;
            }

            if (!IsDashMax(level))
                if (DashLevelsData[level] is T dash)
                    return dash;
            if (!IsHandMax(level))
                if (HandAttackLevelsData[level] is T handAttack)
                    return handAttack;
            if (!IsGunMax(level))
                if (GunAttackLevelData[level] is T gunAttack)
                    return gunAttack;
            if (!IsSuperJumpMax(level))
                if (SuperJumpData[level] is T superJump)
                    return superJump;

            Debug.LogError("Upgrade not found");

            return null;
        }
    }

    public abstract class AbilitySettings
    {
    }
}