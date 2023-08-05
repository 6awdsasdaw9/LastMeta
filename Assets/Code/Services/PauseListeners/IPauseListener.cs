namespace Code.Services.PauseListeners
{
    public interface IPauseListener
    {
        void OnPause();
    }

    public interface IResumeListener: IPauseListener
    {
        void OnResume();
    }
}