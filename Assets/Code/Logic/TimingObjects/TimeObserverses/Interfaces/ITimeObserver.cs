using Code.Data.GameData;

namespace Code.Logic.TimingObjects.TimeObserverses.Interfaces
{
    public interface ITimeObserver
    {
        void OnLoadScene();
        void OnStartTimeOfDay(TimeOfDay timeOfDay);
    }
}