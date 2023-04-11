namespace Code.Services.ScenesEvents
{
    public class StartInteractiveEvents : InteractiveEvent
    {
        private void Start()
        {
            _interactivity.OnStartInteractive += OnStartInteractive;
        }

        private void OnDestroy()
        {
            _interactivity.OnStartInteractive -= OnStartInteractive;
        }

        private void OnStartInteractive()
        {
            _interactivity.OnStartInteractive -= OnStartInteractive;

            EnableFollows(_objectsToEnable);
            DisableFollows(_objectsToDisable);

            Destroy(gameObject);
        }
    }
}