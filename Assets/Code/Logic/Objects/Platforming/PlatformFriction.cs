using Code.Character.Hero.HeroInterfaces;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Platforming
{
    public class PlatformFriction : MonoBehaviour
    {
        [SerializeField] private Rigidbody _platformRb;
    
        private IHero _hero;
        private Rigidbody _connectRb;

        [Inject]
        private void Construct(IHero hero)
        {
            _hero = hero;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(Constants.PlayerTag)) 
                return;
      
            _hero.Collision.SetFrictionPhysicsMaterial();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.CompareTag(Constants.PlayerTag)) 
                return;
        
            _hero.Movement.SetSupportVelocity(_platformRb.velocity );
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.CompareTag(Constants.PlayerTag)) 
                return;
        
            _hero.Collision.SetNoFrictionPhysicsMaterial();
        }
    }
}