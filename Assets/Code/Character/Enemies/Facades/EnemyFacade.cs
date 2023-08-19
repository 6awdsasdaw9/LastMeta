using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Logic.Collisions;
using Code.Services.PauseListeners;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class EnemyFacade: MonoBehaviour, IResumeListener
    {
        [Space, Title("Base components")] 
        public EnemyDeath Death;
        public EnemyType Type;
        public IEnemyStats Stats;
        public EnemyHealth Health;
        public EnemyAudio EnemyAudio;
        public EnemyAnimator Animator;
        public CollisionsController CollisionsController;

        protected EnemyData data;
        protected  IHero hero;
        protected float collisionAttackDamage;
        
        [Inject]
        protected void Construct(DiContainer container)
        {
            hero = container.Resolve<IHero>();
            
            var config = container.Resolve<EnemiesConfig>();
            data = config.GetByType(Type);
            collisionAttackDamage = config.CollisionDamage;
            
            if (hero == null || data == null) return;

            container.Resolve<PauseListenerStorage>().Add(this);

            InitComponents();
        }

        protected abstract void InitComponents();

        public abstract void Die();

        public  abstract void Revival();

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }

        protected  virtual void OnValidate()
        {
            Death = GetComponent<EnemyDeath>();
            EnemyAudio = GetComponentInChildren<EnemyAudio>();
            Health = GetComponent<EnemyHealth>();
            Animator = GetComponent<EnemyAnimator>();
            CollisionsController = GetComponent<CollisionsController>();
        }
    }
}

