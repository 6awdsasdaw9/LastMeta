using Code.Logic.Collisions.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Logic.Common
{
    public class AgentRotateToForfard: FollowTriggerObserver
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private NavMeshAgent _meshAgent;

        private void Update()
        {
            if (IsCorrectRotation())
            {
                return;
            }
            
            FlipSprite();
        }

        private bool IsCorrectRotation()
        {
            return _sprite.flipX == _meshAgent.velocity.x < 0 || _meshAgent.velocity.x == 0;
        }

        private void FlipSprite()
        {
            _sprite.flipX = !_sprite.flipX;
        }

        public override void DisableComponent()
        {
            enabled = false;
        }

        public override void EnableComponent()
        {
            enabled = true;
        }
    }
}