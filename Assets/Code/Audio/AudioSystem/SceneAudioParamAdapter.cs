using Code.Audio;
using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Services.SaveServices;
using Code.UI.HeadUpDisplay;

namespace Code.Services.Adapters.HudAdapters
{
    public class SceneAudioParamAdapter: IEventSubscriber, ISavedData
    {
        private readonly HudFacade _hudFacade;
        private readonly SceneAudioController _audioController;
        private readonly EventsFacade _eventsFacade;
        private readonly IHero _hero;

        public SceneAudioParamAdapter(
            HudFacade hudFacade, 
            SceneAudioController audioController, 
            EventsFacade eventsFacade
            , IHero hero)
        {
            _hudFacade = hudFacade;
            _audioController = audioController;
            _eventsFacade = eventsFacade;
            _hero = hero;
            SubscribeToEvent(true);
        }


        public void SubscribeToEvent(bool flag)
        {
            _hudFacade.Menu.Window.Settings.EffectVolumeHudSlider.OnChangedSliderValue += OnChangedEffectValue;
            _hudFacade.Menu.Window.Settings.MusicVolumeHudSlider.OnChangedSliderValue += OnChangedMusicValue;
            _hero.Health.OnHealthChanged += OnChangeHeroHealthParam;
            _eventsFacade.GameEvents.OnPause += OnPause;
        }

        private void OnChangeHeroHealthParam()
        {
            var healthNormalize = _hero.Health.Current / _hero.Health.Max;
            _audioController.ChancgeHeroHealthParam(healthNormalize);
        }

        private void OnPause(bool isPause)
        {
            _audioController.ChangePauseParam(isPause);
            Logg.ColorLog($"Pause {isPause}");
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
            
        }

        public void SaveData(SavedData savedData)
        {
        }
    }
}