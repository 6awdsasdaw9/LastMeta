using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using UnityEngine;

namespace Code.Character.Enemies.EnemiesFacades
{
    public abstract class EnemyFacade: MonoBehaviour
    {
        public EnemyType Type;
        public IEnemyStats Stats;
        public EnemyHealth Health;
        public EnemyAudio EnemyAudio;
        public EnemyAnimator Animator;

        protected EnemyData data;
        protected  IHero hero;
    }
}

