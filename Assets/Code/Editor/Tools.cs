using System.IO;
using Code;
using Code.Debugers;
using UnityEditor;
using UnityEngine;
using Logger = Code.Debugers.Logger;

public class Tools
{
    [MenuItem("Tools/Delete Progress Data")]
    public static void DeleteProgressData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, Constants.SaveProgressFileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Logger.ColorLog("DELETE PROGRESS DATA");
        }
    }

}