using Code.Character.Common.CommonCharacterInterfaces;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class Enemy: MonoBehaviour, IEnemy
    {
        public IEnemyMovement Movement { get; private set; }
        public IEnemyAttack Attack { get; private set; }
        public ICharacterHealth Health { get; private set; }
        public IEnemyDeath Death { get; private set; }
    }
}