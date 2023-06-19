using Code.Character.Hero;
using Code.Logic.Missile;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public class MissilesFactory
    {
        private readonly Missile.Pool _missilePool;
        
        public MissilesFactory(Missile.Pool missilePool)
        {
            _missilePool = missilePool;
        }

        public Missile SpawnMissile(ShootingParams shootingParams, Vector3 spawnPosition, float forward)
        {
              var missile = _missilePool.Spawn(shootingParams, spawnPosition, forward);
              missile.OnTakeDamage += DeSpawnMissile;
              missile.OnLifetimeCooldownIsUp += DeSpawnMissile;
              
              missile.SetMovement(GetMissileMovement(missile,forward));
              return missile;
        }

        private void DeSpawnMissile(Missile missile)
        {
            missile.OnTakeDamage -= DeSpawnMissile;
            missile.OnLifetimeCooldownIsUp -= DeSpawnMissile;
            missile.Disable();
            _missilePool.Despawn(missile);
        }

        private MissileMovement GetMissileMovement(Missile missile ,float forward)
        {
            if (missile.ShootingParams.MoveType == MissileMoveType.Levitation)
            {
                return new MissileLevitationMovement(missile, forward);
            }
            else  
            {
                return new MissileDirectMovement(missile, forward);
            }
        }
    }
}