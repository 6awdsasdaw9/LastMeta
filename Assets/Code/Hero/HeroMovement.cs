using Code.CameraLogic;
using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rb;
        private RotationController rotate;
        public Vector2 velocity => _rb.velocity;
        private InputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.inputService;
            rotate = new RotationController(transform);
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }
        
        public void FixedUpdate()
        {
            _rb.velocity = new Vector2(_inputService.horizontalAxis, _rb.velocity.y) * _speed * Time.deltaTime;
            rotate.Turn(_inputService.horizontalAxis);
        }

        private void CameraFollow() => _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}