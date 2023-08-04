using System.Collections.Generic;
using Code.Audio;
using Code.Audio.AudioSystem;
using Code.Services.GameTime;
using Zenject;

namespace Code.Services.SaveServices
{
    public class SavedDataStorage 
    {
        private readonly List<ISavedData> _dataSaved = new();
        public IEnumerable<ISavedData> DataSaved => _dataSaved;
        
        private readonly List<ISavedDataReader> _dataReaders = new();
        public IEnumerable<ISavedDataReader> DataReaders => _dataReaders;

        public SavedDataStorage(DiContainer container)
        {
            Add(container.Resolve<GameClock>());
            Add(container.Resolve<SceneAudioController>());
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