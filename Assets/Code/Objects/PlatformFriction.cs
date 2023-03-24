using System;
using Code;
using Code.Character.Hero;
using UnityEngine;
using Zenject;

public class PlatformFriction : MonoBehaviour
{
    [SerializeField] private Rigidbody _platformRb;
    private HeroMovement _heroMovement;
    private Rigidbody _connectRb;


    [Inject]
    private void Construct(HeroMovement heroMovement)
    {
        _heroMovement = heroMovement;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.PlayerTag))
            collision.gameObject.TryGetComponent(out _connectRb);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_connectRb)
            return;
        
        _heroMovement.SetSupportVelocity(_platformRb.velocity);
    }

    private void OnCollisionExit(Collision other)
    {
        _heroMovement.SetSupportVelocity(Vector2.zero);
        
    }
}