using UnityEngine;

namespace Code.Logic.Missile
{
    public abstract class MissileMovement
    {
        protected Missile Missile;
        protected float Forward;
     
        public  MissileMovement(Missile missile, float forward)
        {
            Missile = missile;
            Forward = forward;
        }

        public abstract void StartMove();
        public abstract void StopMove();
    }
}