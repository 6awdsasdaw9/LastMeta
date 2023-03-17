using Code.Services;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack enemyAttack;
        public TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
            triggerObserver.TriggerExit += TriggerExit;
            enemyAttack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            enemyAttack.EnableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            enemyAttack.DisableAttack();
        }
    }
}