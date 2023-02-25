using System;
using Code.Character.Hero;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _dampTime = 0.75f;
        [SerializeField] private bool _isCanLookDown = true;
        private bool _isCanMove = true;

        private Transform _following;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _target;

        private const float offsetX = 0.5f;
        private const float offsetY = 1.7f;

        //private float _inputX => Game.inputService.horizontalAxis;
        [SerializeField] private bool isHavingBounds;
        [SerializeField] private float distanceBoundsX, distanceBoundsY;
        private Vector2 minBounds, maxBounds;

        private float _inputY
        {
            get
            {
                /*if 
                    (_isCanLookDown) return Game.inputService.verticalAxis;*/
                return 0;
            }
        }

        [Inject]
        private void Construct(HeroMovement hero)
        {
            _following = hero.gameObject.transform;
        }

        private void Start()
        {
            _target = GetFollowingPosition();
            transform.position = _target;
            minBounds = new Vector2(-distanceBoundsX - offsetX, -distanceBoundsY);
            maxBounds = new Vector2(distanceBoundsX + offsetX, distanceBoundsY);
        }


        private void LateUpdate()
        {
            Log.ColorLog(IsOutBounds().ToString());
            if (_following == null)
                return;

            if (!IsOutBounds())
            {
                _target = GetFollowingPosition();
            }


            transform.position = Vector3.SmoothDamp(transform.position, _target, ref _velocity, _dampTime);
        }

        private bool IsOutBounds()
        {
            return isHavingBounds && (_following.position.x < minBounds.x || _following.position.x > maxBounds.x);
        }

        private Vector3 GetFollowingPosition()
        {
            return _following.position + _cameraOffset();
        }

        private Vector3 _cameraOffset() => new Vector3(offsetX /* * _inputX*/, offsetY + _inputY, -60f);


        public void CameraHandler(bool active) => _isCanMove = active;

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private void OnDrawGizmos()
        {
            if (!isHavingBounds) return;
            Gizmos.color = new Color32(150, 150, 0, 255);
            Gizmos.DrawRay(Vector3.zero, Vector3.up * distanceBoundsY);
            Gizmos.DrawRay(Vector3.zero, Vector3.down * distanceBoundsY);
            Gizmos.DrawRay(Vector3.zero, Vector3.left * distanceBoundsX);
            Gizmos.DrawRay(Vector3.zero, Vector3.right * distanceBoundsX);
        }
    }
}