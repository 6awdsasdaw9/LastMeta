using Code.Audio;
using Code.Infrastructure.GlobalEvents;
using Code.PresentationModel.HeadUpDisplay;
using Code.Services;

namespace Code.Logic.Adaptors
{
    public class AudioAdapter: IEventSubscriber
    {
        private readonly Hud _hud;
        private readonly SceneAudioController _audioController;
        private readonly EventsFacade _eventsFacade;

        public AudioAdapter(Hud hud, SceneAudioController audioController, EventsFacade eventsFacade)
        {
            _hud = hud;
            _audioController = audioController;
            _eventsFacade = eventsFacade;
            SubscribeToEvent(true);
        }


        public void SubscribeToEvent(bool flag)
        {
            _hud.Menu.Window.EffectVolumeHudSlider.OnChangedSliderValue += OnChangedEffectValue;
            _hud.Menu.Window.MusicVolumeHudSlider.OnChangedSliderValue += OnChangedMusicValue;
            _eventsFacade.GameEvents.OnPause += OnPause;
        }

        private void OnPause(bool isPause)
        {
            _audioController.ChangePauseParam(isPause);
        }

        private void OnChangedMusicValue(float value)
        {
            _audioController.ChangeMusicVolume(value);
        }

        private void OnChangedEffectValue(float value)
        {
            _audioController.ChangeEffectVolume(value);
        }

    }
}