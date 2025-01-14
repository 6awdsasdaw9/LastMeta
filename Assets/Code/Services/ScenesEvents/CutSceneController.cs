using Code.Character.Hero;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.CameraLogic;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace Code.Services.ScenesEvents
{
    public class CutSceneController : MonoBehaviour
    {
      [SerializeField] private PlayableDirector _timelime;
      [SerializeField] private RealCameraFollow realCameraFollow;
      
      private IHero _hero;
      private MovementLimiter _movementLimiter;

      [Inject]
      private void Construct(MovementLimiter limiter,IHero hero)
      {
          _movementLimiter = limiter;
          _hero = hero;
      }
      
      private void OnEnable()
      {
          PlayCutScene();
      }
      private void PlayCutScene()
      {
          _movementLimiter.DisableMovement();
          realCameraFollow.enabled = false;
          _timelime.Play();
      }

      /// <summary>
      /// Timeline Event
      /// </summary>
      public void DisableHero() => 
          _hero.Transform.gameObject.SetActive(false);
    }
}