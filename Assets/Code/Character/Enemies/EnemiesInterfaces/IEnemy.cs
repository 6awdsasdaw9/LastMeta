using Code.Character.Common.CommonCharacterInterfaces;

namespace Code.Character.Enemies
{
    public interface IEnemy
    {
        public IEnemyMovement Movement { get; }
        public IEnemyAttack Attack { get; }
        public ICharacterHealth Health { get; }
        public IEnemyDeath Death { get; }
    }
}