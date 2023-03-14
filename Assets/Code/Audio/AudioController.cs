using UnityEngine;

namespace Code.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private string musicEventPath;

        private FMOD.Studio.EventInstance music;
    
        private FMOD.Studio.PARAMETER_DESCRIPTION _parameterDescription;
        private FMOD.Studio.PARAMETER_ID _parameterID;

        private FMOD.Studio.Bus Music_Volume;
        private FMOD.Studio.Bus Effect_Volume;
    
        private void Start()
        {
            if (musicEventPath == string.Empty)
                return;
            music = FMODUnity.RuntimeManager.CreateInstance(musicEventPath);
            music.start();
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