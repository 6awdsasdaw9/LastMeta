using UnityEngine;

namespace Code.Logic.Missile
{
    public abstract class MissileMovement
    {
        protected HeroMissile heroMissile;
        protected float Forward;
     
        public  MissileMovement(HeroMissile heroMissile, float forward)
        {
            this.heroMissile = heroMissile;
            Forward = forward;
        }

        public abstract void StartMove();
        public abstract void StopMove();
    }
}