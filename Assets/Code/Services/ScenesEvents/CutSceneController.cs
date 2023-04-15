using Code.Character.Hero;
using Code.Logic.CameraLogic;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace Code.Services.ScenesEvents
{
    public class CutSceneController : MonoBehaviour
    {
      [SerializeField] private PlayableDirector _timelime;
      [SerializeField] private CameraFollow _cameraFollow;
      
      private HeroMovement _heroMovement;
      private MovementLimiter _movementLimiter;

      [Inject]
      private void Construct(MovementLimiter limiter,HeroMovement hero)
      {
          _movementLimiter = limiter;
          _heroMovement = hero;
      }
      
      private void OnEnable()
      {
          PlayCutScene();
      }
      private void PlayCutScene()
      {
          _movementLimiter.DisableMovement();
          _cameraFollow.enabled = false;
          _timelime.Play();
      }

      /// <summary>
      /// Timeline Event
      /// </summary>
      public void DisableHero() => 
          _heroMovement.gameObject.SetActive(false);
    }
}