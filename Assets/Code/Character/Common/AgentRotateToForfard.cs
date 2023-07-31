using UnityEngine;
using UnityEngine.AI;

namespace Code.Character.Common
{
    public class AgentRotateToForfard: MonoBehaviour
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
            return _sprite.flipX == _meshAgent.velocity.x < 0;
        }

        private void FlipSprite()
        {
            _sprite.flipX = !_sprite.flipX;
        }
    }
}