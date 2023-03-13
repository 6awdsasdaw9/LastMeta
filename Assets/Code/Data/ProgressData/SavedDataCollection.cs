using System.Collections.Generic;

namespace Code.Data.SavedDataPersistence
{
    public class SavedDataCollection 
    {
        private readonly List<ISavedData> _data = new();
        public IEnumerable<ISavedData> Data => _data;
        
        public void Add(ISavedData savedData) => 
            _data.Add(savedData);

        public void CleanUp() => 
            _data.Clear();
    }
}