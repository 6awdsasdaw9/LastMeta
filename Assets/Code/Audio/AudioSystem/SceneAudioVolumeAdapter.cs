using Code.Services;
using Code.Services.SaveServices;
using Code.UI.HeadUpDisplay;
using Zenject;

namespace Code.Audio.AudioSystem
{
    public class SceneAudioVolumeAdapter: IEventsSubscriber, ISavedDataReader
    {
        private readonly HudFacade _hudFacade;
        private readonly SceneAudioController _audioController;

        public SceneAudioVolumeAdapter(DiContainer container)
        {
            _hudFacade = container.Resolve<HudFacade>();
            _audioController = container.Resolve<SceneAudioController>();
            
            container.Resolve<EventSubsribersStorage>().Add(this);
            container.Resolve<SavedDataStorage>().Add(this);
            
            SubscribeToEvents(true);
        }
        
        public void SubscribeToEvents(bool flag)
        {
            _hudFacade.Menu.Window.Settings.EffectVolumeHudSlider.OnChangedSliderValue += OnChangedEffectValue;
            _hudFacade.Menu.Window.Settings.MusicVolumeHudSlider.OnChangedSliderValue += OnChangedMusicValue;
        }
        
        private void OnChangedMusicValue(float value)
        {
            _audioController.ChangeMusicVolume(value);
        }

        private void OnChangedEffectValue(float value)
        {
            _audioController.ChangeEffectVolume(value);
        }

        public void LoadData(SavedData savedData)
        {
            _hudFacade.Menu.Window.Settings.EffectVolumeHudSlider.SetValue(savedData.AudioVolume.Effects);
            _hudFacade.Menu.Window.Settings.MusicVolumeHudSlider.SetValue(savedData.AudioVolume.Music);
        }
    }
}