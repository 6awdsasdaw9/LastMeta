using Code.Character.Hero.Interfaces;

namespace Code.Character.Hero
{
    public interface IHeroAttack : IDisabled
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