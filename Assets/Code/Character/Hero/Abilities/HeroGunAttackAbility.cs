using System.Threading;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs.HeroConfigs;
using Code.Data.GameData;
using Code.Services;
using Code.Services.Input;

namespace Code.Character.Hero.Abilities
{
    public class HeroGunAttackAbility : Ability
    {
        private readonly IHero _hero;
        private readonly InputService _inputService;
        private readonly HeroConfig _heroConfig;
        public ShootingParams ShootingParams;

        private CancellationTokenSource _abilityCts;

        private readonly Cooldown _abilityCooldown;


        public HeroGunAttackAbility(IHero hero, InputService inputService)
        {
            Type = HeroAbilityType.Gun;
            _hero = hero;
            _inputService = inputService;
            _abilityCooldown = new Cooldown();
            _inputService.OnPressSkillButtonTwo += StartApplying;
        }

        public void SetShootingParams(ShootingParams shootingParams, int level)
        {
            ShootingParams = shootingParams;
            _hero.GunAttack.SetShootingParams(shootingParams);
            _abilityCooldown.SetTime(shootingParams.AttackCooldown);
        }


        public override void StartApplying()
        {
            if (!IsOpen) return;
            _hero.ModeToggle.SetGunMode();
        }

        public override void StopApplying()
        {
        }
    }
}