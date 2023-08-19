using Code.Logic.Collisions.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class TriggerEmemyAttackObserver : MonoBehaviour
    {
        [GUIColor(0.6f,0.1f,0.9f),SerializeField] private TriggerObserver _triggerObserver;
        [GUIColor(0.8f,0.1f,0.4f),SerializeField] private EnemyAttack _enemyAttack;

        private void OnEnable()
        {
            _triggerObserver.OnEnter += OnEnter;
            _triggerObserver.OnExit += OnExit;
            
            _enemyAttack.DisableAttack();
        }

        private void OnDisable()
        {
            _triggerObserver.OnEnter -= OnEnter;
            _triggerObserver.OnExit -= OnExit;
        }

        private void OnEnter(GameObject obj)
        {
            _enemyAttack.EnableAttack();
        }

        private void OnExit(GameObject obj)
        {
            _enemyAttack.DisableAttack();
        }

    }
    
    

}