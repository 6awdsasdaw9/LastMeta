using UnityEngine;

namespace Code.Logic.Missile
{
    public class MissileDirectMovement: MissileMovement
    {
        public MissileDirectMovement(Missile missile, float forward) : base(missile, forward)
        {
            
        }

        public override void StartMove()
        {
            Missile.Rigidbody.velocity = new Vector3(Forward * Missile.ShootingParams.Speed.MaxValue,0,0);
        }

        public override void StopMove()
        {
            
        }
    }
}