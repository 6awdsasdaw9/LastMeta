using System;
using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public abstract class Interactivity : MonoBehaviour, IInteractive
    {
        [SerializeField] protected InteractiveObjectType Type;
        public Action OnStartInteractive;
        public virtual void StartInteractive()
        {
           
        }

        public virtual void StopInteractive()
        {
           
        }
    }
}