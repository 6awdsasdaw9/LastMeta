using System;
using System.Threading;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Code.Logic.Missile
{
    public class MissileLevitationMovement : MissileMovement
    {
        private CancellationTokenSource _cts;
        private bool _isMove;
        private const float _maxVelocityY = 3;

        public MissileLevitationMovement(HeroMissile heroMissile, float forward)
            : base(heroMissile, forward) { }

        public override void StartMove()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            _isMove = true;
            MoveCycle().Forget();
        }

        public override void StopMove()
        {
            _isMove = false;
            _cts?.Cancel();
        }

        private async UniTaskVoid MoveCycle()
        {
            while (_isMove)
            {
                if (heroMissile == null || !heroMissile.gameObject.activeSelf)
                {
                    StopMove();
                    break;
                }

                heroMissile.Rigidbody.AddForce(GetForward(), ForceMode.Impulse);

                CheckMaxVelocityX();
                CheckMaxVelocityY();

                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(0.1f,0.5f)), cancellationToken: _cts.Token);
            }
        }

        private void CheckMaxVelocityY()
        {
            if (heroMissile.Rigidbody.velocity.y is > _maxVelocityY or < _maxVelocityY)
            {
                heroMissile.Rigidbody.velocity = new Vector3(
                    heroMissile.Rigidbody.velocity.x,
                    -heroMissile.Rigidbody.velocity.y * 0.5f,
                    0);
            }
        }

        private void CheckMaxVelocityX()
        {
            if (heroMissile.Rigidbody.velocity.x > heroMissile.ShootingParams.Speed.GetRandom())
            {
                heroMissile.Rigidbody.velocity = new Vector3(
                    heroMissile.ShootingParams.Speed.GetRandom(),
                    heroMissile.Rigidbody.velocity.y,
                    0);
            }
            else if (heroMissile.Rigidbody.velocity.x < heroMissile.ShootingParams.Speed.GetRandom())
            {
                heroMissile.Rigidbody.velocity = new Vector3(
                    -heroMissile.ShootingParams.Speed.GetRandom(),
                    heroMissile.Rigidbody.velocity.y,
                    0);
            }
        }

        private Vector3 GetForward()
        {
            return Vector3.Lerp(heroMissile.Rigidbody.velocity, GetMissileTrajectory(), Time.deltaTime).normalized;
        }

        private Vector3 GetMissileTrajectory()
        {
            return new Vector3(
                Forward * heroMissile.ShootingParams.Speed.GetRandom(),
                heroMissile.ShootingParams.Scatter.GetRandom(),
                0);
        }
    }
}