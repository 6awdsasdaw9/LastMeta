using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using FMODUnity;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAudio : MonoBehaviour, IHeroAudio
    {
        private HeroAudioPath _audioPath;

        [Inject]
        private void Construct(HeroConfig heroConfig)
        {
            _audioPath = heroConfig.AudioPath;
        }
        
        public void PlayStepSound() =>
            PlayOneShotAudio(_audioPath.Step);

        public void PlaySoftStepSound() =>
            PlayOneShotAudio(_audioPath.SoftStep);

        public void PlayOnLandAudio() =>
            PlayOneShotAudio(_audioPath.OnLoad);

        public void PlayPunchAudio() =>
            PlayOneShotAudio(_audioPath.Punch);
        public void PlayDamageAudio() =>
            PlayOneShotAudio(_audioPath.TakeDamage);
        public void PlayJump() => 
            PlayOneShotAudio(_audioPath.Jump);

        public void PlayWaterDeathAudio() => 
            PlayOneShotAudio(_audioPath.WaterDeath);
        
        private void PlayOneShotAudio(EventReference eventReference)
        {
            if (eventReference.IsNull)
                return;

            RuntimeManager.PlayOneShot(eventReference, gameObject.transform.position);
        }
    }
}