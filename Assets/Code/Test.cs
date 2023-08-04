using Code.Audio;
using Code.Audio.AudioSystem;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Test : MonoBehaviour
    {
        private SceneAudioController _sceneAudioController;

        private bool isPause;
        [Inject]
        private void Construct(SceneAudioController sceneAudioController)
        {
            _sceneAudioController = sceneAudioController;
        }

        public void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPause = !isPause;
                _sceneAudioController.ChangePauseParam(isPause);
                Logg.ColorLog($"{isPause}",ColorType.Magenta);
            }*/
        }
    }

}