using Code.Data.Configs;
using Code.Logic.Common.Interfaces;

namespace Code.Character.Enemies.EnemiesInterfaces
{
    public interface IEnemy
    {
        public EnemyType Type { get; }
        public IEnemyMovement Movement { get; }
        public IEnemyAttack Attack { get; }
        public ICharacterHealth Health { get; }
        public IEnemyDeath Death { get; }
    }
}