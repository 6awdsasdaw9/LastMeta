using System.Collections.Generic;
using System.Linq;
using Code.Debugers;
using UnityEngine;

namespace Code.Data.DataPersistence
{
    public class ProgressService : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField] private string _fileName;
        [SerializeField] private bool _useEncryption;

        public ProgressData gameProgressData;
        private FileDataHandler _dataHandler;
        private List<IDataPersistence> _dataPersistenceObjects;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Awake()
        {
           //C:/Users/awdsasdaw/AppData/LocalLow/DefaultCompany
           
            _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName,_useEncryption);
            _dataPersistenceObjects = FindDataPersistenceObjects();
        }

        /*private void OnApplicationQuit()
        {
            SaveGame();
        }*/
        //------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        private void NewProgress() =>
            gameProgressData = new ProgressData(initialScene: Constants.initialScene);
        
        public void LoadProgress()
        {
            // load any saved data from a file using the data handler
            _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataPersistenceObjects = FindDataPersistenceObjects();
            gameProgressData = _dataHandler.Load();

            //if no data can be loaded, initialize to a new game
            if (gameProgressData == null)
            {
                Log.ColorLog("No data was found. Initializing data to defaults.",ColorType.Olive);
                NewProgress();
            }

            //push the loaded data to all other scripts that need it
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameProgressData);
            }
        }

        public void SaveGame()
        {
            _dataHandler ??= new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataPersistenceObjects = FindDataPersistenceObjects();

            if (gameProgressData == null)
            {
                Log.ColorLog("No data was found.A new Game nees to be started before data can be saved",LogStyle.Warning);
                return;
            }
         
            //pass the data to other scripts so they can update it
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(gameProgressData);
            }
            
            //save that data to a file using the data handler
            _dataHandler.Save(gameProgressData);
        }

        public void DeleteGame() => 
            _dataHandler.DeleteGame();

        private List<IDataPersistence> FindDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
                .OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}