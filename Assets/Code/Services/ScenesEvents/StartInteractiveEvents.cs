using UnityEngine;

namespace Code.Services.ScenesEvents
{
    public class StartInteractiveEvents : InteractiveEvent
    {
        private void Start()
        {
            _interactivityHandler.OnStartInteractive += OnStartInteractive;
        }

        private void OnDestroy()
        {
            _interactivityHandler.OnStartInteractive -= OnStartInteractive;
        }

        private void OnStartInteractive()
        {
            _interactivityHandler.OnStartInteractive -= OnStartInteractive;

            EnableFollows(_objectsToEnable).Forget();
            DisableFollows(_objectsToDisable).Forget();

            Destroy(gameObject);
        }
    }
}