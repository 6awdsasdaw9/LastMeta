using System;
using UnityEngine;

namespace Code.Logic.Objects.Interactive.InteractiveObjects
{
    public abstract class Interactivity : MonoBehaviour
    {
        [SerializeField] protected InteractiveObjectType Type;
        
        public Action OnStartInteractive;
        public Action OnStopInteractive;
        public Action OnEndInteractive;
        
        public bool OnAnimationProcess { get; protected set; }

        public abstract void StartInteractive();

        public abstract void StopInteractive();


    }
}