using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Services.SaveServices
{
    public class FileDataHandler
    {
        private readonly string dataDirPath;
        private readonly string dataFileName ;
        private readonly string encryptionCodeWord = "word";
        private bool useEncryption = false;

        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
            this.useEncryption = useEncryption;
        }


        #region  Encrypt
        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
            }
            return modifiedData;
        }
        private string LoadJSONString()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            string loadedData = "";

            //if (File.Exists(fullPath))
            //{
            //    try
            //    {
            //        // load the serialized data from the file


            //        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            //        {
            //            using (StreamReader reader = new StreamReader(stream))
            //            {
            //                dataToLoad = reader.ReadToEnd();
            //            }
            //        }
            //        loadedDate = 
            //        // optionally decrypt the data
            //        if (useEncryption)
            //        {
            //            dataToLoad = EncryptDecrypt(dataToLoad);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            //    }
            //}
            return loadedData;
        }
        #endregion


        public void Save(SavedData savedData)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToStore = JsonConvert.SerializeObject(savedData);

                using (FileStream stream = new FileStream(fullPath,FileMode.Create))
                {
                    using(StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            }
        }
        public SavedData Load()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            SavedData loadedSavedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader  reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    loadedSavedData = JsonConvert.DeserializeObject<SavedData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
                }
            }
            return loadedSavedData;
        }


        public void DeleteGame()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            if (File.Exists(fullPath))
            { 
                File.Delete(fullPath);
            }
        }
    
    }
}