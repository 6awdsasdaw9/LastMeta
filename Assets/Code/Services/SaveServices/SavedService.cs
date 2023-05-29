using Code.Data.Configs;
using Code.Debugers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Data.ProgressData
{
    public class SavedService : MonoBehaviour
    {
        [SerializeField] private bool _useEncryption;
        private const string _fileName = Constants.SaveProgressFileName;
        private FileDataHandler _dataHandler;
        private SavedDataCollection _dataCollection;
        private GameConfig _gameConfig;
        public SavedData SavedData { get; private set; }

        [Inject]
        private void Construct(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void SetSavedDataCollection(SavedDataCollection collection)
        {
            _dataCollection = collection;
        }

        public void LoadProgress()
        {
            LoadData();

            foreach (ISavedData dataPersistenceObj in _dataCollection.Data)
            {
                dataPersistenceObj.LoadData(SavedData);
            }
        }

        public void LoadData()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            SavedData = _dataHandler.Load();

            if (SavedData == null)
            {
                Log.ColorLog("No data was found. Initializing data to defaults.", ColorType.Olive);
                NewProgress();
            }
        }


        [Button]
        public void SaveProgress()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

            if (SavedData == null)
            {
                Log.ColorLog("No data was found.A new Game nees to be started before data can be saved",
                    LogStyle.Warning);
                return;
            }

            SavedData.CurrentScene = SceneManager.GetActiveScene().name;
            foreach (ISavedData dataPersistenceObj in _dataCollection.Data)
            {
                dataPersistenceObj.SaveData(SavedData);
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
                CurrentScene = _gameConfig.initialScene.ToString(),
                HeroHealth =
                {
                    MaxHP = _gameConfig.heroConfig.maxHP
                },
            };

            SavedData.HeroHealth.Reset();
            
            _dataHandler.Save(SavedData);
        }
    }
}