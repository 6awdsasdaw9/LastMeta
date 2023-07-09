using Code.Audio;
using Code.PresentationModel.HeadUpDisplay;
using Code.Services;

namespace Code.Logic.Adaptors
{
    public class AudioAdapter: IEventSubscriber
    {
        private readonly Hud _hud;
        private readonly SceneAudioController _audioController;

        public AudioAdapter(Hud hud, SceneAudioController audioController)
        {
            _hud = hud;
            _audioController = audioController;
            SubscribeToEvent(true);
        }


        public void SubscribeToEvent(bool flag)
        {
            _hud.Menu.Window.EffectVolumeHudSlider.OnChangedSliderValue += OnChangedEffectValue;
            _hud.Menu.Window.MusicVolumeHudSlider.OnChangedSliderValue += OnChangedMusicValue;
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