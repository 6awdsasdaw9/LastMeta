using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;


namespace Code.Audio
{
    public class SceneAudioController 
    {
        private EventReference _ambienceEvent;
        private EventReference _musicEvent;

        private EventInstance _ambience;
        private EventInstance _music;

        private PARAMETER_DESCRIPTION _parameterDescription;
        private PARAMETER_ID _parameterID;

        private Bus Music_Volume;
        private Bus Effect_Volume;


        #region Set Audio EventReference

        public bool IsCurrentAmbienceEvent(EventReference ambienceEvent)
        {
            return _ambienceEvent.Guid == ambienceEvent.Guid;
        }

        public bool IsCurrentMusicEvent(EventReference musicEvent)
        {
            return _musicEvent.Guid == musicEvent.Guid;
        }

        public void SetAmbienceEvent(EventReference ambienceEvent)
        {
            _ambienceEvent = ambienceEvent;
        }

        public void SetMusicEvent(EventReference musicEvent)
        {
            _musicEvent = musicEvent;
        }

        #endregion

        #region Play

        public void PlayAmbience()
        {
            if (_ambienceEvent.IsNull)
                return;

            _ambience = RuntimeManager.CreateInstance(_ambienceEvent);
            _ambience.start();
        }

        public void PlayMusic()
        {
            if (_musicEvent.IsNull)
                return;

            _music = RuntimeManager.CreateInstance(_musicEvent);
            _music.start();
        }

        #endregion

        #region Stop

        public void StopMusic()
        {
            _music.stop(STOP_MODE.ALLOWFADEOUT);
        }

        public void StopAmbience()
        {
            _ambience.stop(STOP_MODE.ALLOWFADEOUT);
        }

        #endregion
    
    
    
        #region Param

        private void SetParam()
        {
            string nameParam = "";
            RuntimeManager.StudioSystem.getParameterDescriptionByName(nameParam, out _parameterDescription);
            _parameterID = _parameterDescription.id;
        }

        //calue = 0 - 1
        private void ChangeParam(float value)
        {
            RuntimeManager.StudioSystem.setParameterByID(_parameterID, value);
        }

        #endregion

        #region Bus

        private void SetBus()
        {
            // Path copy from FMOD
            Music_Volume = RuntimeManager.GetBus("bus:/Master/Music");
            Effect_Volume = RuntimeManager.GetBus("bus:/Master/Effect");
        }

        private void ChangeBusEffect(float volume)
        {
            Effect_Volume.setVolume(volume);
        }

        #endregion

    }
}