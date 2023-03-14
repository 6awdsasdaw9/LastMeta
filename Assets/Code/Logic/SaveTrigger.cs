using Code.Data.ProgressData;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private PersistentSavedDataService _dataService;

        [Inject]
        private void Construct(PersistentSavedDataService dataService)
        {
            _dataService = dataService;
        }

        private void OnTriggerEnter(Collider other)
        {
            _dataService.SaveProgress();
            Log.ColorLog("Progress Save In Trigger",ColorType.Aqua);
            gameObject.SetActive(false);
        }
    }
}