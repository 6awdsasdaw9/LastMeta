using Code.Audio;
using Code.Infrastructure.GlobalEvents;
using Code.UI.HeadUpDisplay;

namespace Code.Services.Adapters.HudAdapters
{
    public class SceneAudioAdapter: IEventSubscriber
    {
        private readonly HudFacade _hudFacade;
        private readonly SceneAudioController _audioController;
        private readonly EventsFacade _eventsFacade;

        public SceneAudioAdapter(HudFacade hudFacade, SceneAudioController audioController, EventsFacade eventsFacade)
        {
            _hudFacade = hudFacade;
            _audioController = audioController;
            _eventsFacade = eventsFacade;
            SubscribeToEvent(true);
        }


        public void SubscribeToEvent(bool flag)
        {
            _hudFacade.Menu.Window.Settings.EffectVolumeHudSlider.OnChangedSliderValue += OnChangedEffectValue;
            _hudFacade.Menu.Window.Settings.MusicVolumeHudSlider.OnChangedSliderValue += OnChangedMusicValue;
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