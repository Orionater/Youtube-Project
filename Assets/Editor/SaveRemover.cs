using UnityEditor;
using UnityEngine;
using System.IO;

public class SaveRemover : Editor
{
    [MenuItem("Save Remover",menuItem = "Save Remover/Remove Save")]
    static void removeSave()
    {
        string path = Application.dataPath + "/Saves";
        string saveData = path + "/Game.dat";
        if (Directory.Exists(path))
        {
            if (File.Exists(saveData))
            {
                File.Delete(saveData);
                MonoBehaviour.print("Removed savefile.");
            }
        }
    }
}
