using Code.Data.GameData;

namespace Code.Logic.Objects.TimingObjects.TimeObserverses.Interfaces
{
    public interface ITimeObserver
    {
        void OnLoadScene();
        void OnStartTimeOfDay(TimeOfDay timeOfDay);
    }
}