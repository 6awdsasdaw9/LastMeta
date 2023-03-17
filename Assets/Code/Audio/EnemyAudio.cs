using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
   public StudioEventEmitter emitter;

   public void PlayStep()
   {
      emitter.Event = "event:/SFX/Bullets/Bullet_Bubble";
   }
   
}
