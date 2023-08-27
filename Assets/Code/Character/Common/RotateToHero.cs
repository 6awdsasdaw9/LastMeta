using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Collisions;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Graphics;
using UnityEngine;
using Zenject;

namespace Code.Logic.Common
{
    public class RotateToHero : FollowTriggerObserver
    {
        [SerializeField] private SpriteFlipper _spriteFlipper;
        [SerializeField] private CollisionsController _collisionsController;

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
            _spriteFlipper.Flip(_isLoockLeft);
            _collisionsController?.Flip(_isLoockLeft);
            OnFlipLeft?.Invoke(_isLoockLeft);
        }

        private bool IsCorrectRotation() =>
            _isLoockLeft == !(transform.position.x < _heroTransform.position.x);

        private void OnValidate()
        {
            _spriteFlipper.Validate(gameObject);
            _collisionsController = GetComponent<CollisionsController>();

        }
    }
}