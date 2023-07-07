using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
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

        private PARAMETER_DESCRIPTION _dayNightParameterDescription;
        private PARAMETER_ID _dayNightParameterID;
        
        private PARAMETER_DESCRIPTION _pauseParameterDescription;
        private PARAMETER_ID _pauseParameterID;

        private Bus _music_Volume;
        private Bus Effect_Volume;


        public SceneAudioController()
        {
            SetNightParam();
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

        private void StopMusic() => _music.stop(STOP_MODE.ALLOWFADEOUT);

        private void StopAmbience() => _ambience.stop(STOP_MODE.ALLOWFADEOUT);

        #endregion
        
        #region Param
        private void SetNightParam()
        {
            const string nameParam = "DayNight";
            RuntimeManager.StudioSystem.getParameterDescriptionByName(nameParam, out _dayNightParameterDescription);
            _dayNightParameterID = _dayNightParameterDescription.id;
        }
        
        //calue = 0 - 1
        public void ChangeNightParam(bool isNight)
        {
            var value = isNight ? 1 : 0;
            RuntimeManager.StudioSystem.setParameterByID(_dayNightParameterID, value);
        }

        #endregion

        #region Snapshot
        
        private EventInstance _pauseSnapshotInstance;
        private bool _isActivePauseSnapshot;
        private bool _isInit;
        
        public void InitSnapshot(string pauseSnapshot)
        {
            /*if(_isInit)
                return;
            _isInit = true;
            _pauseSnapshotInstance = RuntimeManager.CreateInstance(pauseSnapshot);
            _pauseSnapshotInstance.stop(STOP_MODE.ALLOWFADEOUT);*/
            //_pauseSnapshotInstance.start();
        }
        
        //calue = 0 - 1
        public void ChangePauseParam(bool isPause)
        {
            /*if(isPause == _isActivePauseSnapshot)
                return;

            _isActivePauseSnapshot = isPause;
            if (isPause)
            {
                _pauseSnapshotInstance.stop(STOP_MODE.ALLOWFADEOUT);
                _pauseSnapshotInstance.setParameterByName("GamePauseStatus", 1);
            }
            else
            {
                _pauseSnapshotInstance.start();
                _pauseSnapshotInstance.setParameterByName("GamePauseStatus", 0);
            }*/
        }

        #endregion
        #region Bus

        private void SetBus()
        {
            // Path copy from FMOD
            _music_Volume = RuntimeManager.GetBus("bus:/Master/Music");
            Effect_Volume = RuntimeManager.GetBus("bus:/Master/Effect");
        }

        private void ChangeEffectVolume(float volume) => Effect_Volume.setVolume(volume);
        private void ChangeMusicVolume(float volume) => _music_Volume.setVolume(volume);

        #endregion

    }
}