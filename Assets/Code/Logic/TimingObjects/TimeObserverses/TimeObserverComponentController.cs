using System.Collections.Generic;
using UnityEngine;

namespace Code.Logic.TimingObjects.TimeObserverses
{
  
    public class TimeObserverComponentController : TimeObserver
    {
        [SerializeField] private List<MonoBehaviour> _enabledComponents;
        [SerializeField] private List<GameObject> _enableObjects;
        [SerializeField] private List<MonoBehaviour> _disableComponents;
        [SerializeField] private List<GameObject> _disableObjects;

        /*[Space] 
        [SerializeField] private AudioEvent _enableAudioEvent; 
        [SerializeField] private AudioEvent _disableAudioEvent; */
        
        protected override void StartReaction()
        {
            /*_enableAudioEvent.PlayAudioEvent();*/
            SetActiveComponent(_enabledComponents,true);
            SetActiveComponent(_disableComponents,false);
            SetActiveObjects(_enableObjects,true);
            SetActiveObjects(_disableObjects,false);
        }

        protected override void EndReaction()
        {
            /*_disableAudioEvent.PlayAudioEvent();*/
            SetActiveComponent(_enabledComponents,false);
            SetActiveComponent(_disableComponents,true);
            SetActiveObjects(_enableObjects,false);
            SetActiveObjects(_disableObjects,true);
        }

        private void SetActiveObjects(List<GameObject> list, bool isActive)
        {
            foreach (var obj in list)
            {
                obj.SetActive(isActive);
            }
        }

        private void SetActiveComponent(List<MonoBehaviour> list, bool isActive)
        {
            foreach (var component in list)
            {
                component.enabled = isActive;
            }
        }
    }
}