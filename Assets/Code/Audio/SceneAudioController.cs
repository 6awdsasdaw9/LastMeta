using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;


namespace Code.Audio
{
    public class SceneAudioController 
    {
        private readonly EventsFacade _eventsFacade;
        private EventReference _ambienceEvent;
        private EventReference _musicEvent;

        private EventInstance _ambience;
        private EventInstance _music;

        private PARAMETER_DESCRIPTION _nightParameterDescription;
        private PARAMETER_ID _nightParameterID;
        
        private PARAMETER_DESCRIPTION _pauseParameterDescription;
        private PARAMETER_ID _pauseParameterID;

        private Bus Music_Volume;
        private Bus Effect_Volume;


        public SceneAudioController()
        {
            SetNightParam();
            SetPauseParam();
        }
        #region Set Audio EventReference

        private bool IsCurrentAmbienceEvent(EventReference ambienceEvent) => _ambienceEvent.Guid == ambienceEvent.Guid;

        private bool IsCurrentMusicEvent(EventReference musicEvent) => _musicEvent.Guid == musicEvent.Guid;

        private void SetAmbienceEvent(EventReference ambienceEvent) => _ambienceEvent = ambienceEvent;

        private void SetMusicEvent(EventReference musicEvent) => _musicEvent = musicEvent;

        #endregion

        #region Play

        public void ChangeSceneAudio(EventReference music ,EventReference ambience )
        {
            if (ambience.IsNull)
            {
                StopAmbience();
            }
            else if(!IsCurrentAmbienceEvent(ambience))
            {
                StopAmbience();
                SetAmbienceEvent(ambience);
                PlayAmbience();
            }

            if (music.IsNull)
            {
                StopMusic();
            }
            else if(!IsCurrentMusicEvent(music))
            {
                StopMusic();
               SetMusicEvent(music);
               PlayMusic();
            }
        }

        private void PlayAmbience()
        {
            if (_ambienceEvent.IsNull)
                return;

            _ambience = RuntimeManager.CreateInstance(_ambienceEvent);
            _ambience.start();
        }

        private void PlayMusic()
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

        private void SetNightParam()
        {
            string nameParam = "NightToggle";
            RuntimeManager.StudioSystem.getParameterDescriptionByName(nameParam, out _nightParameterDescription);
            _nightParameterID = _nightParameterDescription.id;
        }
        
        //calue = 0 - 1
        public void ChangeNightParam(bool isNight)
        {
            var value = isNight ? 1 : 0;
            RuntimeManager.StudioSystem.setParameterByID(_nightParameterID, value);
        }

        private void SetPauseParam()
        {
            string nameParam = "GamePauseStatus";
            RuntimeManager.StudioSystem.getParameterDescriptionByName(nameParam, out _pauseParameterDescription);
            _pauseParameterID = _pauseParameterDescription.id;
        }
        
        //calue = 0 - 1
        public void ChangePauseParam(bool isPause)
        {
            var value = isPause ? 1 : 0;
            RuntimeManager.StudioSystem.setParameterByID(_pauseParameterID, value);
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