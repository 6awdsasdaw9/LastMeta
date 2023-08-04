using Code.Character.Common.CommonCharacterInterfaces;
using Code.Character.Enemies.EnemiesInterfaces;
using Code.Data.Configs;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class Enemy: MonoBehaviour, IEnemy
    {
        public EnemyType Type { get; }
        public IEnemyMovement Movement { get; private set; }
        public IEnemyAttack Attack { get; private set; }
        public ICharacterHealth Health { get; private set; }
        public IEnemyDeath Death { get; private set; }
    }
}