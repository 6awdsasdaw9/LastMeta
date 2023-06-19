using System;
using System.Threading;
using Code.Debugers;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Code.Logic.Missile
{
    public class MissileLevitationMovement : MissileMovement
    {
        private CancellationTokenSource _cts;
        private bool _isMove;
        private float startY;

        private float _maxVelocityY = 3;

        public MissileLevitationMovement(Missile missile, float forward)
            : base(missile, forward)
        {
        }

        public override void StartMove()
        {
            startY = Missile.transform.position.y;
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
                if (Missile == null || !Missile.gameObject.activeSelf)
                {
                    StopMove();
                    break;
                }

                Missile.Rigidbody.AddForce(GetForward());
                while (Missile.Rigidbody.velocity.y > _maxVelocityY)
                {
                    Missile.Rigidbody.velocity -= new Vector3(-Missile.Rigidbody.velocity.x * 0.3f,Time.deltaTime,0);
                    await UniTask.DelayFrame(1, cancellationToken: _cts.Token);
                }
                
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _cts.Token);
            }
        }

        private Vector3 GetForward()
        {
            return Vector3.Lerp(Missile.Rigidbody.velocity , GetMissileTrajectory(), Time.deltaTime).normalized;
        }

        private Vector3 GetMissileTrajectory()
        {
            return new Vector3(
                Forward * Missile.ShootingParams.Speed.GetRandom() * 1.5f,
                startY + Missile.ShootingParams.Scatter.GetRandom(),
                0);
        }
    }
}