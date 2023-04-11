namespace Code.Services.ScenesEvents
{
    public class EndInteractiveEvent : InteractiveEvent
    {
        private void Start()
        {
            _interactivity.OnEndInteractive += OnEndInteractive;
        }

        private void OnDestroy()
        {
            _interactivity.OnEndInteractive -= OnEndInteractive;
        }

        private void OnEndInteractive()
        {
            _interactivity.OnStartInteractive -= OnEndInteractive;

            EnableFollows(_objectsToEnable);
            DisableFollows(_objectsToDisable);

            Destroy(gameObject);
        }
    }
}