using UnityEngine;

namespace Code.Audio
{
    public class EnemyAudio : MonoBehaviour
    {

        public string path;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                FMODUnity.RuntimeManager.PlayOneShot(path,gameObject.transform.position);
            }
       
     
        }

        public void AudioPlayBreath()
        {
            //emitter.EventReference = EventReference.Find("vent:/SFX/Bullets/Bullet_Bubble");

      
        }

   
    }
}
