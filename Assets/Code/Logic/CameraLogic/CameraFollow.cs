using Code.Character.Hero;
using UnityEngine;
using Zenject;

namespace Code.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _dampTime = 0.75f;
        [SerializeField] private bool _isCanLookDown = true;
        [SerializeField] private bool _isCanMoveY = true;
        private float startPosY;
        private bool _isCanMove = true;

        private Transform _following;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _target;

        private const float offsetX = 0.5f;
        private const float offsetY = 1.6f;

        [SerializeField] private bool isHavingBounds;
        [SerializeField] private float distanceBoundsX, distanceBoundsY;
        private Vector2 minBounds, maxBounds;


        [Inject]
        private void Construct(HeroMovement hero)
        {
            _following = hero.transform;
        }

        private void Awake()
        {
            if (!_isCanMoveY)
                startPosY = transform.position.y;

            minBounds = new Vector2(-distanceBoundsX - offsetX, -distanceBoundsY);
            maxBounds = new Vector2(distanceBoundsX + offsetX, distanceBoundsY);
         
            _target = GetFollowingPosition();
            transform.position = _target;
        }

        private void LateUpdate()
        {
            if (_following == null || !_isCanMove)
            {
                return;
            }

            var x = _following.position.x + _cameraOffset.x;
            var y = _following.position.y + _cameraOffset.y;
            var z = _cameraOffset.z;

            if (isHavingBounds)
            {
                CheckBounds(ref x, ref y);
            }

            _target = new Vector3(x, y, z);
            transform.position = Vector3.SmoothDamp(transform.position, _target, ref _velocity, _dampTime);
        }

        private void CheckBounds(ref float x, ref float y)
        {
            x = Mathf.Clamp(x, minBounds.x, maxBounds.x);
            y = _isCanMoveY ? Mathf.Clamp(y, minBounds.y, maxBounds.y) : startPosY;
        }

        private Vector3 GetFollowingPosition()
        {
            return _following.position + _cameraOffset;
        }

        private Vector3 _cameraOffset => new Vector3(offsetX, offsetY, -60f);

        public void CameraHandler(bool active)
        {
            _isCanMove = active;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private void OnDrawGizmos()
        {
            if (!isHavingBounds)
            {
                return;
            }

            Gizmos.color = new Color32(150, 150, 0, 255);
            Gizmos.DrawRay(Vector3.zero, Vector3.up * distanceBoundsY);
            Gizmos.DrawRay(Vector3.zero, Vector3.down * distanceBoundsY);
            Gizmos.DrawRay(Vector3.zero, Vector3.left * distanceBoundsX);
            Gizmos.DrawRay(Vector3.zero, Vector3.right * distanceBoundsX);
        }
    }
}