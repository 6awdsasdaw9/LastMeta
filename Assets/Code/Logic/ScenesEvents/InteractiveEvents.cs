using System.Collections.Generic;
using Code.Logic.Interactive.InteractiveObjects;
using UnityEngine;

namespace Code.Logic.ScenesEvents
{
    public class InteractiveEvents: MonoBehaviour
    {
        [SerializeField] private Interactivity _interactivity;
        [SerializeField] private List<GameObject> _interactivityFollows;

        private void Start()
        {
            _interactivity.OnStartInteractive += OnStartInteractive;
            DisableFollows();
        }

        private void OnStartInteractive()
        {
            EnableFollows();
        }

        private void EnableFollows()
        {
            foreach (var i in _interactivityFollows)
            {
                i.SetActive(true); 
            }
        }

        private void DisableFollows()
        {
            foreach (var i in _interactivityFollows)
            {
                i.SetActive(false); 
                
            }
        }
    }
}