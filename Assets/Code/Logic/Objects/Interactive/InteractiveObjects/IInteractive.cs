namespace Code.Logic.Objects.Interactive.InteractiveObjects
{
    public interface IInteractive
    {
        public bool OnProcess { get; }
        public void StartInteractive();
        public void StopInteractive();
    }
}