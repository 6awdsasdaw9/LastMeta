using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Data.DataPersistence
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager instance { get; private set; }

        [Header("File Storage Config")]
        [SerializeField] private string _fileName;
        [SerializeField] private bool _useEncryption;

        private ProgressData _gameProgressData;
        private FileDataHandler _dataHandler;
        private List<IDataPersistence> _dataPersistenceObjects;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
           //C:/Users/awdsasdaw/AppData/LocalLow/DefaultCompany/Tamagochi
            InitializedInstance();
            _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName,_useEncryption);
            _dataPersistenceObjects = FindDataPersistenceObjects();
            LoadGame(); 
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void InitializedInstance()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            instance = this;
        }

        public void NewGame()
        {
            _gameProgressData = new ProgressData();
        }

        public void LoadGame()
        {
            // load any saved data from a file using the data handler
            _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataPersistenceObjects = FindDataPersistenceObjects();
            _gameProgressData = _dataHandler.Load();

            //if no data can be loaded, initialize to a new game
            if (this._gameProgressData == null)
            {
                Debug.Log("No data was found. Initializing data to defaults.");
                NewGame();
            }

            //push the loaded data to all other scripts that need it
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(_gameProgressData);
            }
        }

        public void SaveGame()
        {
            if (_dataHandler == null) _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataPersistenceObjects = FindDataPersistenceObjects();

            if (_gameProgressData == null)
            {
                Debug.LogWarning("No data was found.A new Game nees to be started before data can be saved");
                return;
            }
            //pass the data to other scripts so they can update it
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(_gameProgressData);
            }
            //save that data to a file using the data handler
            _dataHandler.Save(_gameProgressData);
        }

        public void DeleteGame()
        {
            // if (_fileName != name || _dataHandler == null) _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataHandler.DeleteGame();
        }
        private List<IDataPersistence> FindDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}