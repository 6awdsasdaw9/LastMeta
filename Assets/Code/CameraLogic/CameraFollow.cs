using Code.Infrastructure;
using UnityEngine;

namespace Code.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _dampTime = 0.75f;
        private bool _isCanMove = true;
        private bool _isCanLookDown = true;
       [SerializeField] private Transform _following;

        private Vector3 _velocity = Vector3.zero;

        private float _offsetX = 1.5f;
        private float _offsetY = 1f;
        //private float _inputX => Game.inputService.horizontalAxis;

        private float _inputY
        {
            get
            {
          //      if (_isCanLookDown) return Game.inputService.verticalAxis;
                return 0;
            }
        }

        private void LateUpdate()
        {
            if (_following == null) return;

            Vector3 _cameraPostion = _following.position + _cameraOffset();
            transform.position = Vector3.SmoothDamp(transform.position, _cameraPostion, ref _velocity, _dampTime);
        }

        private Vector3 _cameraOffset() => new Vector3(_offsetX/* * _inputX*/, _offsetY + _inputY, -60f);

        public void CameraHandler(bool active) => _isCanMove = active;

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }
    }
}