using System;
using Code.Logic.Collisions;
using Code.Logic.Collisions.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Logic.Common
{
    public class AgentRotateToForfard: FollowTriggerObserver
    {
        [SerializeField] private NavMeshAgent _meshAgent;
        
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private CollidersRotater _collidersRotater;

        private bool _isLookLeft;
        public Action<bool> OnFlipLeft;

        private void Update()
        {
            if (IsCorrectRotation())
            {
                return;
            }
            
            _isLookLeft = !_isLookLeft;
            FlipSprite();
            FlipObjectRoot();
            OnFlipLeft?.Invoke(_isLookLeft);
        }

        private bool IsCorrectRotation()
        {
            return _isLookLeft == _meshAgent.velocity.x < 0 || _meshAgent.velocity.x == 0;
        }

        private void FlipSprite()
        {
            if (_sprite == null)
            {
                return;
            }
            _sprite.flipX = _isLookLeft;
        }

        private void FlipObjectRoot()
        {
            if (_collidersRotater == null)
            {
                return;
            }
            _collidersRotater.Flip();
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