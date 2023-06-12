using System.Collections.Generic;
using Code.Logic.DayOfTime;

namespace Code.Services.SaveServices
{
    public class SavedDataStorage 
    {
        private readonly List<ISavedData> _data = new();
        public IEnumerable<ISavedData> Data => _data;
        
        public SavedDataStorage(GameClock gameClock)
        {
            Add(gameClock);
        }
        
        public void Add(ISavedData savedData) => 
            _data.Add(savedData);

        public void CleanUp() => 
            _data.Clear();
    }
}