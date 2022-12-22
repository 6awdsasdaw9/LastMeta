using System.Collections;
using System.Collections.Generic;
using Code.Character;
using Code.Character.Hero;
using UnityEngine;
using UnityEngine.InputSystem;

public class checkJump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;

    private HeroMovement _controller;
    private Rigidbody2D _body;
    private HeroCollision _ground;
    private Vector2 _velocity;

    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;

    private bool _desiredJump, _onGround;


    // Start is called before the first frame update
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<HeroCollision>();
        _controller = GetComponent<HeroMovement>();

        _defaultGravityScale = 1f;
    }

    // Update is called once per frame
    
    public void OnJump(InputAction.CallbackContext context)
    {
        
            if (context.started)
            {
                _desiredJump = true;
              
            }

            if (context.canceled)
            {
                _desiredJump = false;
            }
        
    }

    void Update()
    {
        //_desiredJump |= _controller.input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.onGround;
        _velocity = _body.velocity;

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_body.velocity.y > 0)
        {
            _body.gravityScale = _upwardMovementMultiplier;
        }
        else if (_body.velocity.y < 0)
        {
            _body.gravityScale = _downwardMovementMultiplier;
        }
        else if (_body.velocity.y == 0)
        {
            _body.gravityScale = _defaultGravityScale;
        }

        _body.velocity = _velocity;
    }

    private void JumpAction()
    {
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase += 1;

            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_body.velocity.y);
            }

            _velocity.y += _jumpSpeed;
        }
    }
}
