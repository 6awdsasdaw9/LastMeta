using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Character.Enemies
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [ShowInInspector,ReadOnly] protected bool IsActive { get; private set; }
        public void DisableAttack() => IsActive = false;
        public void EnableAttack() => IsActive = true;
    }

    public enum MelleAttackType
    {
        None,
        Default
    }

    //------------------------------------------------------------------------------------------------------------------
    public abstract class EnemyMelleAttackBase : EnemyAttack
    {
        protected abstract void StartAttack();

        /// <summary>
        /// For Animation Event
        /// </summary>
        protected abstract void OnAttack();

        /// <summary> 
        /// For Animation Event
        /// </summary>
        protected abstract void OnAttackEnded();
    }

    public enum RangeAttackType
    {
        None,
        Default,
        Spike
    }
    //------------------------------------------------------------------------------------------------------------------

    public abstract class EnemyRangeAttackBase : EnemyAttack
    {
        protected abstract void StartRangeAttack();

        /// <summary>
        /// For Animation Event
        /// </summary>
        protected abstract void OnRangeAttack();

        /// <summary> 
        /// For Animation Event
        /// </summary>
        protected abstract void OnRangeAttackEnded();
    }

    public abstract class EnemyCollisionAttackBase : EnemyAttack
    {
        protected abstract void StartCollisionAttack();
    }
}