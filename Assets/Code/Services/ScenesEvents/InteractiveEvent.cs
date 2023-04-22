using System;
using System.Collections.Generic;
using Code.Logic.Interactive.InteractiveObjects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.ScenesEvents
{
    public class InteractiveEvent : MonoBehaviour
    {
        [SerializeField] protected Interactivity _interactivityHandler;
        [SerializeField] private float _enableDelayTime;
        [SerializeField] private float _disableDelayTime;
        [SerializeField] protected List<GameObject> _objectsToEnable;
        [SerializeField] protected List<GameObject> _objectsToDisable;
        
        protected async UniTaskVoid EnableFollows(List<GameObject> objects)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_enableDelayTime));
            
            foreach (var i in objects)
            {
                i.SetActive(true);
            }
        }

        protected  async UniTaskVoid DisableFollows(List<GameObject> objects)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_disableDelayTime));
            
            foreach (var i in objects)
            {
                i.SetActive(false);
            }
        }
    }
}