using Code.Data.GameData;
using Code.Logic.Common.Interfaces;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAttack : IDisabledComponent
    {
        public bool IsAttack { get; }
        DamageParam DamageParam { get; }
        void SetDamageParam(DamageParam damageParam);

        /// <summary>
        /// Animation Event
        /// </summary>
        void OnAttack();

        /// <summary>
        /// Animation Event
        /// </summary>
        void OnAttackEnded();
    }

    public interface IHeroRangeAttack : IDisabledComponent
    {
        public bool IsAttack { get; }
        ShootingParams ShootingParams { get; }
        void SetShootingParams(ShootingParams shootingParams);
        void StartAttack();
        void StopAttack();
    }
}