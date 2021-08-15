using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingM : MonoBehaviour
{
    public static SavingM Save;
    string path;
    string dataFile = "/Game.dat";
    private void Start()
    {
        if(Save != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Save = this;
            DontDestroyOnLoad(this);
        }
        path = Application.dataPath + "/Saves"; // If you are trying to make this for multiple platforms. I'd recommend using: "Path.Combine()", But I'm only making this for windows.
        FileCheck();
    }
    public void FileCheck()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    public void SaveGame(GameData savedata)
    {
        if (!File.Exists(path + dataFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path + dataFile);
            bf.Serialize(file, savedata);
            file.Close();
        }
        else
        {
            File.Delete(path + dataFile);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path + dataFile);
            bf.Serialize(file, savedata);
            file.Close();
        }
    }
    public GameData LoadGame()
    {
        if (File.Exists(path + dataFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path + dataFile, FileMode.Open);
            GameData saveData = (GameData)bf.Deserialize(file);
            file.Close();
            return saveData;
        }
        return null;
    }
}
[Serializable]
public class GameData
{
    public float x, y;
    public GameData(Vector2 playPos)
    {
        x = playPos.x;
        y = playPos.y;
    }
}
