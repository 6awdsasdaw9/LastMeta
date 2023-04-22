using System;
using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public abstract class Interactivity : MonoBehaviour, IInteractive
    {
        [SerializeField] protected InteractiveObjectType Type;
        public Action OnStartInteractive;
        public Action OnEndInteractive;
        public bool OnProcess { get; protected set; }

        public abstract void StartInteractive();

        public abstract void StopInteractive();
        
    }
}