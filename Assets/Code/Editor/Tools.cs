using System.IO;
using Code;
using Code.Debugers;
using UnityEditor;
using UnityEngine;

public class Tools
{
    [MenuItem("Tools/Delete Progress Data")]
    public static void DeleteProgressData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, Constants.SaveProgressFileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Logg.ColorLog("DELETE PROGRESS DATA");
        }
    }

}