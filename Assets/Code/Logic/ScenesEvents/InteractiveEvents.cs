using System.Collections.Generic;
using Code.Logic.Interactive.InteractiveObjects;
using UnityEngine;

namespace Code.Logic.ScenesEvents
{
    public class InteractiveEvents: MonoBehaviour
    {
        [SerializeField] private Interactivity _interactivity;
        [SerializeField] private List<GameObject> _objectsToEnable;
        [SerializeField] private List<GameObject> _objectsToDisable;

        private void Start()
        {
            _interactivity.OnStartInteractive += OnStartInteractive;
        }

        private void OnStartInteractive()
        {
            _interactivity.OnStartInteractive -= OnStartInteractive;
            
            EnableFollows(_objectsToEnable);
            DisableFollows(_objectsToDisable);
            
            Destroy(gameObject);
        }

        private void EnableFollows(List<GameObject> objects)
        {
            foreach (var i in objects)
            {
                i.SetActive(true); 
            }
        }

        private void DisableFollows(List<GameObject> objects)
        {
            foreach (var i in objects)
            {
                i.SetActive(false); 
                
            }
        }
    }
}