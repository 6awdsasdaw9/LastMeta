using System;
using Code;
using Code.Character.Hero;
using UnityEngine;
using Zenject;

public class PlatformFriction : MonoBehaviour
{
    [SerializeField] private Rigidbody _platformRb;
    
    private HeroMovement _heroMovement;
    private HeroCollision _heroCollision;
    private Rigidbody _connectRb;
    
    [Inject]
    private void Construct(HeroMovement heroMovement)
    {
        _heroMovement = heroMovement;
        _heroCollision = heroMovement.GetComponent<HeroCollision>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(Constants.PlayerTag)) 
            return;
        
        collision.gameObject.TryGetComponent(out _connectRb);
        _heroCollision.SetFrictionPhysicsMaterial();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_connectRb)
            return;
        
        _heroMovement.SetSupportVelocity(_platformRb.velocity);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag(Constants.PlayerTag)) 
            return;
        
        _heroMovement.SetSupportVelocity(Vector2.zero);
        _heroCollision.SetNoFrictionPhysicsMaterial();
        
    }
}