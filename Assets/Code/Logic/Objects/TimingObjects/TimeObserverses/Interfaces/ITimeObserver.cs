using Code.Data.GameData;

namespace Code.Logic.DayOfTime.Interfaces
{
    public interface ITimeObserver
    {
        void OnLoadScene();
        void OnStartTimeOfDay(TimeOfDay timeOfDay);
    }
}