using Code.Character.Common.CommonCharacterInterfaces;

namespace Code.Character.Hero.HeroInterfaces
{
    public interface IHeroAttack : IDisabledComponent
    {
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