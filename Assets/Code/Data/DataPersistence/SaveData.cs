using System.Collections.Generic;

namespace Code.Data.DataPersistence
{
    public class SaveData 
    {
        private readonly List<IDataPersistence> _data = new();
        public IEnumerable<IDataPersistence> Data => _data;

        public void Add(IDataPersistence data) => 
            _data.Add(data);
    }
}