using Code.Character.Hero;
using Code.Logic.Triggers;
using UnityEngine;
using Zenject;

namespace Code.Character.Common
{
    public class RotateToHero : FollowTriggerObserver
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Transform _heroTransform;

        [Inject]
        private void Construct(HeroMovement hero)
            => _heroTransform = hero.transform;
    
        private void Update()
        {
            if (_heroTransform)
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            _spriteRenderer.flipX = !(transform.position.x < _heroTransform.position.x);
        }

    }
}