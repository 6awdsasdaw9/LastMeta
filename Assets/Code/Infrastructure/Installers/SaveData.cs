using System.Collections.Generic;
using Code.Data.DataPersistence;

namespace Code.Infrastructure.Installers
{
    public class SaveData 
    {
        private readonly List<IDataPersistence> _data = new();
        public IEnumerable<IDataPersistence> Data => _data;

        public void Add(IDataPersistence data)
        {
            _data.Add(data);
        }
    }
}