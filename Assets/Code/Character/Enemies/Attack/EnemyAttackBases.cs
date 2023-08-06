using UnityEngine;

namespace Code.Character.Enemies
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        protected bool IsActive { get; private set; }
        public void DisableAttack() => IsActive = false;
        public void EnableAttack() => IsActive = true;
        
    }

    public enum MelleAttackType
    {
        None,
        Default
    }
    public abstract class EnemyMelleAttackBase : EnemyAttack
    {
        protected abstract void StartAttack();
        
        /// <summary>
        /// Animation Event
        /// </summary>
        protected abstract void OnAttack();

        /// <summary> 
        /// Animation Event
        /// </summary>
        protected abstract void OnAttackEnded();
        
    }
    
    public enum RangeAttackType
    {
        None,
        Default,
        Spike
    }
    public abstract class EnemyRangeAttackBase : EnemyAttack
    {
        protected abstract void StartRangeAttack();
        
        /// <summary>
        /// Animation Event
        /// </summary>
        protected abstract void OnRangeAttack();

        /// <summary> 
        /// Animation Event
        /// </summary>
        protected abstract void OnRangeAttackEnded();
    }
}