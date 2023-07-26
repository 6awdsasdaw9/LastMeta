using System.Collections.Generic;
using Code.Services.GameTime;

namespace Code.Services.SaveServices
{
    public class SavedDataStorage 
    {
        private readonly List<ISavedData> _dataSaved = new();
        public IEnumerable<ISavedData> DataSaved => _dataSaved;
        
        private readonly List<ISavedDataReader> _dataReaders = new();
        public IEnumerable<ISavedDataReader> DataReaders => _dataReaders;

        public SavedDataStorage(GameClock gameClock)
        {
            Add(gameClock);
        }
        
        public void Add(ISavedData savedData) => _dataSaved.Add(savedData);
        public void Add(ISavedDataReader savedData) => _dataReaders.Add(savedData);

        public void CleanUp()
        {
            _dataSaved.Clear();
            _dataReaders.Clear();
        }
    }
}