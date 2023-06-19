using System.Threading;
using Code.Character.Hero;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services;
using Code.Services.Input;

namespace Code.Character.Hero
{
    public class HeroGunAttackAbility : Ability
    {
        private readonly IHero _hero;
        private readonly InputService _inputService;
        private readonly HeroConfig _heroConfig;
        public ShootingParams Params;

        private CancellationTokenSource _abilityCts;

        private readonly Cooldown _abilityCooldown;


        public HeroGunAttackAbility(IHero hero, InputService inputService)
        {
            Type = HeroAbilityType.Gun;

            _hero = hero;
            _inputService = inputService;

            _abilityCooldown = new Cooldown();
        }

        public void SetShootingParams(ShootingParams shootingParams)
        {
            Params = shootingParams;
            _hero.GunAttack.SetShootingParams(shootingParams);
            _abilityCooldown.SetTime(shootingParams.AttackCooldown);
        }

        public override void OpenAbility()
        {
            if (IsOpen)
                return;

            IsOpen = true;
            _inputService.OnPressSkillButtonTwo += StartApplying;
        }

        public override void StartApplying()
        {
            _hero.ModeToggle.SetGunMode();
        }

        public override void StopApplying()
        {
        }
    }
}