namespace Code.Services.ScenesEvents
{
    public class EndInteractiveEvent : InteractiveEvent
    {
        private void Start()
        {
            _interactivityHandler.OnEndInteractive += OnEndInteractive;
        }

        private void OnDestroy()
        {
            _interactivityHandler.OnEndInteractive -= OnEndInteractive;
        }

        private void OnEndInteractive()
        {
            _interactivityHandler.OnStartInteractive -= OnEndInteractive;
            
            EnableFollows(_objectsToEnable).Forget();
            DisableFollows(_objectsToDisable).Forget();
            Destroy(gameObject);
        }
    }
}