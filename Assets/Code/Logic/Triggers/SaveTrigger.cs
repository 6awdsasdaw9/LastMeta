using Code.Data.ProgressData;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Logic.Triggers
{
    public class SaveTrigger : MonoBehaviour
    {
        private SavedService _service;

        [Inject]
        private void Construct(SavedService service)
        {
            _service = service;
        }

        private void OnTriggerEnter(Collider other)
        {
            _service.SaveProgress();
            Log.ColorLog("Progress Save In Trigger",ColorType.Aqua);
            gameObject.SetActive(false);
        }
    }
}