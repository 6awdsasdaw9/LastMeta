namespace Code.Services
{
    public interface IEventSubscriber
    {
        void SubscribeToEvent(bool flag);
    }
}