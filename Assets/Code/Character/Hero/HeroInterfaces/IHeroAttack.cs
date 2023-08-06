using Code.Data.GameData;
using Code.Logic.Common.Interfaces;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAttack : IDisabledComponent
    {
        public bool IsAttack { get; }
        AttackData AttackData { get; }
        void SetDamageParam(AttackData attackData);

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