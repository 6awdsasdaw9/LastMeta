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
      [Inject] private HeroMovement _heroMovement; 
      
      private void OnEnable()
      {
          PlayCutScene();
      }


      private void PlayCutScene()
      {
          _cameraFollow.enabled = false;
          _timelime.Play();
      }

      public void DisableHero()
      {
          _heroMovement.gameObject.SetActive(false);
      }
    }
}