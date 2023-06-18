using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAbility : MonoBehaviour, IHeroAbility
    {
        public HeroAbilityLevelData AbilityLevelData { get; private set; }

        private IHero _hero;
        private HeroConfig _heroConfig;
        private InputService _inputService;

        private HeroDashAbility _heroDashAbility;
        private HeroHandAttackAbility _heroHandAttackAbility;
        private HeroGunAttackAbility _heroGunAttackAbility;

        [Inject]
        private void Construct(InputService inputService, HeroConfig heroConfig)
        {
            _inputService = inputService;
            _heroConfig = heroConfig;
            _hero = GetComponent<IHero>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                LevelUpDash();
            }
        }

        public void Init(HeroAbilityLevelData abilityLevelData)
        {
            AbilityLevelData = abilityLevelData;
            OpenDash(AbilityLevelData.DashLevel);
            OpenHandAttack(AbilityLevelData.HandLevel);
            OpenGunAttack(AbilityLevelData.GunLevel);
        }

        private void OpenGunAttack(int level)
        {
            _heroGunAttackAbility = new HeroGunAttackAbility(_hero, _inputService);
          //  _heroGunAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[level]);
            _heroGunAttackAbility.OpenAbility();
        }

        [Button]
        public void LevelUpHandAttack()
        {
            AbilityLevelData.HandLevel++;
            _heroHandAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[ AbilityLevelData.HandLevel]);
        }

        public void OpenHandAttack(int level = 0)
        {
            _heroHandAttackAbility = new HeroHandAttackAbility(_hero, _inputService);
            _heroHandAttackAbility.SetData(_heroConfig.AbilitiesParams.HandAttackLevelsData[level]);
            _heroHandAttackAbility.OpenAbility();
        }

        [Button]
        public void LevelUpDash()
        {
            if (_heroDashAbility == null)
            {
                OpenDash();
            }
            else
            {
                AbilityLevelData.DashLevel++;
                _heroDashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[AbilityLevelData.DashLevel]);
            }
        }

        public void OpenDash(int level = 0)
        {
            _heroDashAbility = new HeroDashAbility(_hero, _inputService);
            _heroDashAbility.SetData(_heroConfig.AbilitiesParams.DashLevelsData[level]);
            _heroDashAbility.OpenAbility();
        }

        private void OnDisable()
        {
            _heroDashAbility?.StopApplying();
        }
    }

    [Serializable]
    public class HeroAbilityLevelData
    {
        public int DashLevel;
        public int GunLevel;
        public int HandLevel;
    }
}