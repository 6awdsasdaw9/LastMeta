using Code.Character.Common.CommonCharacterInterfaces;
using Code.Data.GameData;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAttack : IDisabledComponent
    {
        void SetDamageParam(DamageParam damageParam);
        void Attack();

        /// <summary>
        /// Animation Event
        /// </summary>
        void OnAttack();

        /// <summary>
        /// Animation Event
        /// </summary>
        void OnAttackEnded();
    }
}