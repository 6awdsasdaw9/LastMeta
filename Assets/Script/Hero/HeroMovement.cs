using System;
using Script.Infrastructure;
using Script.Services.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class HeroMovement: MonoBehaviour
    { 
        [SerializeField] private  float _speed;
        [SerializeField] private Rigidbody2D _rb;
        private RotationController rotate;
        public  Vector2 velocity => _rb.velocity;
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
        }

        public void FixedUpdate()
        {
            _rb.velocity =  new Vector2(_inputService.horizontalAxis, _rb.velocity.y) * _speed* Time.deltaTime;
            rotate.Turn(_inputService.horizontalAxis);
        }
        
        
    }
}