using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;


namespace Code.Audio
{
    public class SceneAudioController : MonoBehaviour
    {
        [SerializeField] private EventReference _ambienceEvent;
        [SerializeField] private EventReference _musicEvent;
        
        private EventInstance _ambience;
        private EventInstance _music;
        
        private PARAMETER_DESCRIPTION _parameterDescription;
        private PARAMETER_ID _parameterID;

        private Bus Music_Volume;
        private Bus Effect_Volume;
        
        private void Start()
        {
            PlayMusic();
            PlayAmbience();
        }

        private void OnDisable()
        {
           _ambience.stop(STOP_MODE.ALLOWFADEOUT);
           _music.stop(STOP_MODE.ALLOWFADEOUT);
        }

        
        private void PlayAmbience()
        {
            if(_ambienceEvent.IsNull)
                return;
            
            _ambience = RuntimeManager.CreateInstance(_ambienceEvent);
            _ambience.start();
        }

        private void PlayMusic()
        {
            if(_musicEvent.IsNull)
                return;
            
            _music = RuntimeManager.CreateInstance(_musicEvent);
            _music.start();
        }
        
        #region Param
        private void SetParam()
        {
            string nameParam = "";
            FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName(nameParam, out _parameterDescription);
            _parameterID = _parameterDescription.id;
        }

        //calue = 0 - 1
        private void ChangeParam(float value)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(_parameterID, value);
        }

        #endregion

        #region Bus

        private void SetBus()
        {
            // Path copy from FMOD
            Music_Volume = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
            Effect_Volume = FMODUnity.RuntimeManager.GetBus("bus:/Master/Effect");
        }

        private void ChangeBusEffect(float volume)
        {
            Effect_Volume.setVolume(volume);
        }
        #endregion
    
    }
}