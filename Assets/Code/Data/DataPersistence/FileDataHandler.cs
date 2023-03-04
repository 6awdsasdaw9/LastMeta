using System;
using System.IO;
using UnityEngine;

namespace Code.Data.DataPersistence
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

    

        public void Save(ProgressData progressData)
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToStore = JsonUtility.ToJson(progressData, true);

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
        public ProgressData Load()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            ProgressData loadedProgressData = null;
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
                    loadedProgressData = JsonUtility.FromJson<ProgressData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
                }
            }
            return loadedProgressData;
        }


        public void DeleteGame()
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            if (File.Exists(fullPath))
            { 
                File.Delete(fullPath);
                Console.WriteLine("File deleted.");
            }
        }
    
    }
}