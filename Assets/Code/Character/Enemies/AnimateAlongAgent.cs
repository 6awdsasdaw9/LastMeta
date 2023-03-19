using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private  EnemyAnimator animator;
        [SerializeField] private EnemyAttack _attack;
        
        private const float minimalVelocity = 0.05f;
        private void Update()
        {
            if (ShouldMove() && !_attack.attackIsActive)
            {
                animator.PlayMove(agent.velocity.magnitude);
            }
            else
            {
                animator.StopMoving();
            }
        }

        private bool ShouldMove() =>
            agent.velocity.magnitude > minimalVelocity && agent.remainingDistance > agent.radius;
    }
}