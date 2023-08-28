using System;
using Code.Logic.Collisions;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Graphics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Logic.Common
{
    public class AgentRotateToForfard : FollowTriggerObserver
    {
        [SerializeField] private NavMeshAgent _meshAgent;

        [Space, Title("Optional components")] [SerializeField]
        private SpriteFlipper _spriteFlipper;

        [SerializeField] private CollisionsController _collisionsController;

        private bool _isLookLeft;
        public Action<bool> OnFlipLeft;
        

        public void TryFlip()
        {
            if (IsCorrectRotation())
            {
                return;
            }

            _isLookLeft = !_isLookLeft;
            _spriteFlipper.Flip(_isLookLeft);
            _collisionsController?.Flip();
            OnFlipLeft?.Invoke(_isLookLeft); 
        }

        private bool IsCorrectRotation() => 
            _isLookLeft == _meshAgent.velocity.x < 0 || _meshAgent.velocity.x == 0;

        public override void DisableComponent()
        {
            enabled = false;
        }

        public override void EnableComponent()
        {
            enabled = true;
        }

        private void OnValidate()
        {
            _spriteFlipper.Validate(gameObject);
            TryGetComponent(out _collisionsController);
        }
    }
}