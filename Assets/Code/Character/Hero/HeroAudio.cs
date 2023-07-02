using System;
using Code.Character.Hero.HeroInterfaces;
using FMODUnity;
using UnityEngine;

namespace Code.Character.Hero
{
    public class HeroAudio : MonoBehaviour, IHeroAudio
    {
        private const string pathStep = "event:/SFX/Player/Player_Step";
        private const string pathSoftStep = "event:/SFX/Player/Player_StepSoft";
        private const string pathTakeDamage = "event:/SFX/Player/Player_Damage";
        private const string pathDash = "event:/SFX/Player/Player_Dash";

        private const string pathJump = "event:/SFX/Player/Player_Jump";
        private const string pathOnLand = "event:/SFX/Player/Player_OnLand";
        private const string pathPunch = "event:/SFX/Player/Player_Punch";
        private const string pathShoot = "event:/SFX/Player/Player_Shoot";
        private const string pathStunned = "event:/SFX/Player/Player_Stunned";
        private const string pathWaterDeath = "event:/SFX/Player/Shark Death";
        
        public void PlayStepSound() =>
            RuntimeManager.PlayOneShot(pathStep, transform.position);

        public void PlaySoftStepSound() =>
            RuntimeManager.PlayOneShot(pathSoftStep, transform.position);

        public void PlayOnLandAudio() =>
            RuntimeManager.PlayOneShot(pathOnLand, transform.position);

        public void PlayPunchAudio() =>
            RuntimeManager.PlayOneShot(pathPunch, transform.position);

        public void PlayDamageAudio() =>
            RuntimeManager.PlayOneShot(pathTakeDamage, transform.position);
        

        public void PlayJump()
        {
            RuntimeManager.PlayOneShot(pathJump, transform.position);
        }

        public void PlayWaterDeathAudio()
        {
            RuntimeManager.PlayOneShot(pathWaterDeath, transform.position);
        }
        
        public void PlayOneShotAudio(string path)
        {
            if (path == string.Empty)
                return;

            RuntimeManager.PlayOneShot(path, gameObject.transform.position);
        }
    }
}