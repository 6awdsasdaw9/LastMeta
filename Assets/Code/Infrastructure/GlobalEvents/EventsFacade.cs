namespace Code.Infrastructure.GlobalEvents
{
    public class EventsFacade
    {
        public SceneEvents SceneEvents { get; } = new();
        public TimeEvents TimeEvents { get; } = new();
        
    }
}