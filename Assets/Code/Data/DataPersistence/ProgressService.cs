using Code.Debugers;
using Code.Infrastructure.Installers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Data.DataPersistence
{
    public class ProgressService : MonoBehaviour
    {
        //C:\Users\awdsasdaw\AppData\LocalLow\DefaultCompany\LastMeta
        
        [Title("File Storage Config")] 
        [SerializeField] private string _fileName;
        [SerializeField] private bool _useEncryption;
        private FileDataHandler _dataHandler;
        public ProgressData gameProgressData { get; private set; }
        [Inject] private SaveData _data;

        
        private void NewProgress()
        {
            gameProgressData = new ProgressData(initialScene: Constants.homeScene);
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataHandler.Save(gameProgressData);
        }

        public void LoadProgress()
        {
            LoadData();

            foreach (IDataPersistence dataPersistenceObj in _data.Data)
            {
                dataPersistenceObj.LoadData(gameProgressData);
            }
        }

        public void LoadData()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            gameProgressData = _dataHandler.Load();

            if (gameProgressData == null)
            {
                Log.ColorLog("No data was found. Initializing data to defaults.", ColorType.Olive);
                NewProgress();
            }
        }

        [Button]
        public void SaveProgress()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            if (gameProgressData == null)
            {
                Log.ColorLog("No data was found.A new Game nees to be started before data can be saved",
                    LogStyle.Warning);
                return;
            }
            
            foreach (IDataPersistence dataPersistenceObj in _data.Data)
            {
                dataPersistenceObj.SaveData(gameProgressData);
            }

            _dataHandler.Save(gameProgressData);
        }

        [Button]
        public void DeleteProgress() =>
            _dataHandler.DeleteGame();
    }
}