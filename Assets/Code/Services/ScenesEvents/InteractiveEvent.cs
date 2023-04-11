using System.Collections.Generic;
using Code.Logic.Interactive.InteractiveObjects;
using UnityEngine;

namespace Code.Services.ScenesEvents
{
    public class InteractiveEvent : MonoBehaviour
    {
        [SerializeField] protected Interactivity _interactivity;
        [SerializeField] protected List<GameObject> _objectsToEnable;
        [SerializeField] protected List<GameObject> _objectsToDisable;

        protected void EnableFollows(List<GameObject> objects)
        {
            foreach (var i in objects)
            {
                i.SetActive(true);
            }
        }

        protected void DisableFollows(List<GameObject> objects)
        {
            foreach (var i in objects)
            {
                i.SetActive(false);
            }
        }
    }
}