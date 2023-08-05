using Code.Data.Configs;
using Code.Data.Configs.HeroConfigs;
using Code.Debugers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Services.SaveServices
{
    public class SavedService : MonoBehaviour
    {
        [SerializeField] private bool _useEncryption;
        private const string _fileName = Constants.SaveProgressFileName;
        private FileDataHandler _dataHandler;
        private SavedDataStorage _dataStorage;
        private HeroConfig _heroConfig;
        private ScenesConfig _scenesConfig;
        public SavedData SavedData { get; private set; }

        [Inject]
        private void Construct(HeroConfig heroConfig, ScenesConfig scenesConfig)
        {
            _heroConfig = heroConfig;
            _scenesConfig = scenesConfig;
        }

        public void SetSavedDataStorage(SavedDataStorage storage)
        {
            _dataStorage = storage;
        }

        public void LoadProgress()
        {
            LoadData();

            foreach (ISavedData savedData in _dataStorage.DataSaved)
            {
                savedData.LoadData(SavedData);
            }
            foreach (ISavedDataReader dataReader in _dataStorage.DataReaders)
            {
                dataReader.LoadData(SavedData);
            }
        }

        public void LoadData()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            SavedData = _dataHandler.Load();

            if (SavedData == null)
            {
                Logg.ColorLog("No data was found. Initializing data to defaults.", ColorType.Olive);
                NewProgress();
            }
        }


        [Button]
        public void SaveProgress()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            if (SavedData == null)
            {
                Logg.ColorLog("No data was found.A new Game nees to be started before data can be saved",
                    LogStyle.Warning);
                return;
            }

            SavedData.CurrentScene = SceneManager.GetActiveScene().name;
            foreach (ISavedData savedData in _dataStorage.DataSaved)
            {
                savedData.SaveData(SavedData);
            }
            _dataHandler.Save(SavedData);
        }

        [Button]
        public void DeleteProgress()
        {
            _dataHandler.DeleteGame();
        }

        private void NewProgress()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            SavedData = new SavedData
            {
                CurrentScene = _scenesConfig.InitialScene.ToString(),
                HeroHealth =
                {
                    MaxHP = _heroConfig.HeroParams.maxHP
                },
            };

            SavedData.HeroHealth.Reset();
            
            _dataHandler.Save(SavedData);
        }
    }
}