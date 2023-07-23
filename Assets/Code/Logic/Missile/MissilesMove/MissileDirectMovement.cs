using UnityEngine;

namespace Code.Logic.Missile.MissilesMove
{
    public class MissileDirectMovement: MissileMovement
    {
        public MissileDirectMovement(HeroMissile heroMissile, float forward) : base(heroMissile, forward)
        {
            
        }

        public override void StartMove()
        {
            heroMissile.Rigidbody.velocity = new Vector3(Forward * heroMissile.ShootingParams.Speed.MaxValue,0,0);
        }

        public override void StopMove()
        {
            
        }
    }
}