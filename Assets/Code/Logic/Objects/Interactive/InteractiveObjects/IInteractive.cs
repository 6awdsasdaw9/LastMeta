namespace Code.Logic.Interactive.InteractiveObjects
{
    public interface IInteractive
    {
        public bool OnProcess { get; }
        public void StartInteractive();
        public void StopInteractive();
    }
}