using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float minimalVelocity = 0.05f;
        public NavMeshAgent agent;
        public EnemyAnimator animator;

        private void Update()
        {
            if (ShouldMove())
            {
                animator.Move(agent.velocity.magnitude);
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