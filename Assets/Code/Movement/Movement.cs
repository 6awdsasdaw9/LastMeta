using System;
using UnityEngine;

namespace Code.Movement
{
    [Serializable]
    public class Movement
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] [Range(0.1f,2)]private float _speed = 5;

        private float _movementSmoothing = 0.05f;
        private Vector3 thisVelocity = Vector3.zero;
    

        public void HorizontalMove(Vector2 vector2)
        {
            var targetVelocity = new Vector2(vector2.x * _speed, _rigidbody.velocity.y);
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref thisVelocity, _movementSmoothing);
        }
    
        public bool IsNear(Transform target)=>
            Vector2.Distance(_rigidbody.position, target.position) <= Constants.distance;

    }
}