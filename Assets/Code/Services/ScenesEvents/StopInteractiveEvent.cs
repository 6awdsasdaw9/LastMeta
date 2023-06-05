namespace Code.Services.ScenesEvents
{
    public class StopInteractiveEvent : InteractiveEvent
    {
        private void Start()
        {
            _interactivityHandler.OnStopInteractive += OnStopInteractive;
        }

        private void OnDestroy()
        {
            _interactivityHandler.OnStopInteractive -= OnStopInteractive;
        }

        private void OnStopInteractive()
        {
            _interactivityHandler.OnStopInteractive -= OnStopInteractive;
            
            EnableFollows(_objectsToEnable).Forget();
            DisableFollows(_objectsToDisable).Forget();
            Destroy(gameObject);
        }
    }
}