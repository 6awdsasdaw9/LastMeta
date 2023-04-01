using UnityEngine;

namespace Code.Character.Hero
{

    public class HeroAudio : MonoBehaviour
    {
        private const string pathStep = "event:/SFX/Player/Player_Step";
        private const string pathSoftStep = "event:/SFX/Player/Player_StepSoft";
        private const string pathTakeDamage = "event:/SFX/Player/Player_Damage";
        private const string pathDash = "event:/SFX/Player/Player_Dash";
        private const string pathOnLand = "event:/SFX/Player/Player_OnLand";
        private const string pathPunch = "event:/SFX/Player/Player_Punch";
        private const string pathShoot = "event:/SFX/Player/Player_Shoot";
        private const string pathStunned = "event:/SFX/Player/Player_Stunned";
        
        public void PlayStepSound() => 
            FMODUnity.RuntimeManager.PlayOneShot(pathStep,gameObject.transform.position);
        
        public void PlaySoftStepSound() => 
            FMODUnity.RuntimeManager.PlayOneShot(pathSoftStep,gameObject.transform.position);

        public void PlayOnLandAudio() => 
            FMODUnity.RuntimeManager.PlayOneShot(pathOnLand,gameObject.transform.position);
        
        public void PlayPunchAudio() => 
            FMODUnity.RuntimeManager.PlayOneShot(pathPunch,gameObject.transform.position);

        public void PlayDamageAudio() => 
            FMODUnity.RuntimeManager.PlayOneShot(pathTakeDamage,gameObject.transform.position);
    }
}