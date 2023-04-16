using Code.Character.Hero;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Logic.CameraLogic
{
    public class CameraLimiter : MonoBehaviour
    {
        [SerializeField] private CameraFollow _cameraFollow;

        [SerializeField] private float _cameraRaycastDistance = 0.2f;
        [SerializeField] private float _heroRaycastDistance = 4;

        [SerializeField] private Cooldown _cooldown;

        private LayerMask _stopLayerMask;
        private Camera _camera;
        private Transform _hero;


        public bool _heroRaycast, _cameraRaycast;

        [Inject]
        private void Construct(HeroMovement heroMovement)
        {
            _hero = heroMovement.transform;
        }

        private void Start()
        {
            _camera = Camera.main;
            _stopLayerMask = 1 << LayerMask.NameToLayer(Constants.StopCameraLayer);
        }

        private void LateUpdate()
        {
            if (_cooldown.IsUp())
            {
                CheckHero();
            }
            else
            {
                _cooldown.UpdateCooldown();
            }
            // CheckLeftSide();
            // CheckRightSide();
        }

        #region Two Side

        private void CheckHero()
        {
            _heroRaycast = Physics.Raycast(_hero.position, Vector3.left, _heroRaycastDistance, _stopLayerMask)
                           || Physics.Raycast(_hero.position, Vector3.right, _heroRaycastDistance, _stopLayerMask);

            _cameraRaycast = Physics.Raycast(GetLeftPoint(), Vector3.left, _cameraRaycastDistance, _stopLayerMask)
                             || Physics.Raycast(GetRightPoint(), Vector3.right, _cameraRaycastDistance, _stopLayerMask);
            if (_cameraRaycast)
            {
                _cameraFollow.StopFollow();
            }

            if (!_heroRaycast)
            {
                _cameraFollow.StartFollow();
                _cooldown.ResetCooldown();
            }

            Debug.DrawRay(_hero.position, Vector3.left * _heroRaycastDistance);
            Debug.DrawRay(_hero.position, Vector3.right * _heroRaycastDistance);
        }

        #endregion


        #region Left Side

        private Vector3 GetLeftPoint()
        {
            var leftPoint = _camera.ScreenToWorldPoint(new Vector3(0, _camera.pixelHeight / 2, 0));
            leftPoint.z = 0;
            return leftPoint;
        }


        private Vector3 GetRightPoint()
        {
            var rightPoint = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight / 2, 0));
            rightPoint.z = 0;
            return rightPoint;
        }

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            _camera = Camera.main;

            Gizmos.DrawRay(GetRightPoint(), Vector3.right * _cameraRaycastDistance);
            Gizmos.DrawRay(GetLeftPoint(), Vector3.left * _cameraRaycastDistance);
        }
    }
}