using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Collisions.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Logic.Common
{
    public class RotateToHero : FollowTriggerObserver
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Transform _heroTransform;
        private bool _isLoockLeft;

        public Action<bool> OnFlipLeft;

        [Inject]
        private void Construct(IHero hero)
        {
            _heroTransform = hero.Transform;
        }

        private void Update()
        {
            if (_heroTransform && !IsCorrectRotation())
            {
                RotateTowardsHero();
            }
        }

        private void RotateTowardsHero()
        {
            _isLoockLeft = !(transform.position.x < _heroTransform.position.x);
            _spriteRenderer.flipX = _isLoockLeft;
            OnFlipLeft?.Invoke(_isLoockLeft);
        }

        private bool IsCorrectRotation() =>
            _spriteRenderer.flipX == !(transform.position.x < _heroTransform.position.x);
    }
}