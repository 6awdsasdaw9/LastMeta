using System;
using FMOD.Studio;
using UnityEngine;

namespace Code.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private string musicEventPath;

        private EventInstance music;
    
        private PARAMETER_DESCRIPTION _parameterDescription;
        private PARAMETER_ID _parameterID;

        private Bus Music_Volume;
        private Bus Effect_Volume;
    
        private void Start()
        {
            if (musicEventPath == string.Empty)
                return;
            music = FMODUnity.RuntimeManager.CreateInstance(musicEventPath);
            music.start();
        }

        private void OnDisable()
        {
            music.stop(STOP_MODE.ALLOWFADEOUT);
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