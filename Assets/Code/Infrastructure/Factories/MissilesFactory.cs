using Code.Character.Hero;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Logic.Missile;
using Code.Logic.Missile.MissilesMove;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public class MissilesFactory
    {
        private readonly HeroMissile.Pool _missilePool;
        
        public MissilesFactory(HeroMissile.Pool missilePool)
        {
            _missilePool = missilePool;
        }

        public HeroMissile SpawnMissile(ShootingParams shootingParams, IHero hero)
        {
              var missile = _missilePool.Spawn(shootingParams,hero);
              missile.OnTakeDamage += DeSpawnMissile;
              missile.OnLifetimeCooldownIsUp += DeSpawnMissile;
              
              missile.SetMovement(GetMissileMovement(missile, hero.Transform.localScale.x));
              return missile;
        }

        private void DeSpawnMissile(HeroMissile heroMissile)
        {
            heroMissile.OnTakeDamage -= DeSpawnMissile;
            heroMissile.OnLifetimeCooldownIsUp -= DeSpawnMissile;
            heroMissile.Disable();
            _missilePool.Despawn(heroMissile);
        }

        private MissileMovement GetMissileMovement(HeroMissile heroMissile ,float forward)
        {
            if (heroMissile.ShootingParams.MoveType == MissileMoveType.Levitation)
            {
                return new MissileLevitationMovement(heroMissile, forward);
            }
            else  
            {
                return new MissileDirectMovement(heroMissile, forward);
            }
        }
    }
}