using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObject : MonoBehaviour, IInteractive
    {
        public bool OnProcess { get; }

        public void StartInteractive()
        {
            throw new System.NotImplementedException();
        }

        public void StopInteractive()
        {
            throw new System.NotImplementedException();
        }
    }
}