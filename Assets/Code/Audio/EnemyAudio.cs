using FMODUnity;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
   public StudioEventEmitter emitter;

   public void AudioPlayStep()
   {

      emitter.EventReference = EventReference.Find("event:/SFX/Bullets/Bullet_Bubble");
      
      emitter.Play();
   }
   
   public void AudioPlayBreath()
   {
      emitter.Event = "event:/SFX/Bullets/Bullet_Bubble";
   }

   
}
