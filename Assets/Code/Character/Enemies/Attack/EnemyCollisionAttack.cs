using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Collisions;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemyCollisionAttack : EnemyCollisionAttackBase, IEventsSubscriber
    {
        [SerializeField] private CollisionObserver _colliderObserver;
        private CollisionAttackData _data;
        private IHero _hero;
        private float _collisionDamage;

        public void Init(IHero hero, CollisionAttackData data, float collisionDamage)
        {
            _data = data;
            _hero = hero;
            _collisionDamage = collisionDamage; 
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }
        
        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _colliderObserver.OnEnter += OnEnter;
            }
            else
            {
                _colliderObserver.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(GameObject collision)
        {
            _hero.EffectsController.Push((_hero.Transform.position - transform.position).normalized * _data.PushForce);
            _hero.Health.TakeDamage(_collisionDamage * _data.DamageMultiplayer);
            Logg.ColorLog("OnEnter");
        }


        protected override void StartCollisionAttack()
        {
        }
    }
}