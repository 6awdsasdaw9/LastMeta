using UnityEngine;

namespace Code.Audio
{
    public class AudioPoint : MonoBehaviour
    {
        [SerializeField] private bool _isPlaysConstantly;
        [SerializeField] private string _path;

        private FMOD.Studio.EventInstance _audio;
        private void Start()
        {
            if (_isPlaysConstantly && _path != "")
            {
                _audio = FMODUnity.RuntimeManager.CreateInstance(_path);
                _audio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                _audio.start();
            }
        }

        public void FinishConstanlyAudio()
        {
            if (_isPlaysConstantly && _path != "")
            {
                _audio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        public void SoundOneShot(string soundEvent)
        {
            if(soundEvent != "") FMODUnity.RuntimeManager.PlayOneShot(soundEvent,gameObject.transform.position);
        }

        public void PathOneShot()
        {
            if(_path != "") FMODUnity.RuntimeManager.PlayOneShot(_path, gameObject.transform.position);
        }

        private void OnDisable()
        {
            if (_isPlaysConstantly) _audio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}