using Code.Data.GameData;
using Code.Debugers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Data.ProgressData
{
    public class PersistentSavedDataService : MonoBehaviour
    {
        //C:\Users\awdsasdaw\AppData\LocalLow\DefaultCompany\LastMeta
        [Title("File Storage Config")] [SerializeField]
        private string _fileName;

        [SerializeField] private bool _useEncryption;
        private FileDataHandler _dataHandler;
        public SavedData savedData { get; private set; }
        SavedDataCollection dataCollection;
        private ConfigData _configData;
        
        [Inject]
        private void Construct(ConfigData configData)
        {
            _configData = configData;
        }

        public void LoadProgress()
        {
            LoadData();

            foreach (ISavedData dataPersistenceObj in dataCollection.Data)
            {
                dataPersistenceObj.LoadData(savedData);
            }
        }

        public void LoadData()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            savedData = _dataHandler.Load();

            if (savedData == null)
            {
                Log.ColorLog("No data was found. Initializing data to defaults.", ColorType.Olive);
                NewProgress();
            }
        }

        [Button]
        public void SaveProgress()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            if (savedData == null)
            {
                Log.ColorLog("No data was found.A new Game nees to be started before data can be saved",LogStyle.Warning);
                return;
            }
            
            foreach (ISavedData dataPersistenceObj in dataCollection.Data)
            {
                dataPersistenceObj.SaveData(savedData);
            }

            _dataHandler.Save(savedData);
        }

        public void SetSavedDataCollection(SavedDataCollection collection)
        {
            dataCollection = collection;
        }

        [Button]
        public void DeleteProgress() =>
            _dataHandler.DeleteGame();

        private void NewProgress()
        {
            savedData = new SavedData();
            savedData.heroPositionData.level = _configData.initialScene.ToString();
            savedData.heroHealth.maxHP = _configData.heroConfig.maxHP;
            savedData.heroHealth.Reset();
            
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataHandler.Save(savedData);
        }
    }
}