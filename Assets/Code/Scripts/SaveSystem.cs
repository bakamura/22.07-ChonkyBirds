using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    private static string progressPath = Application.persistentDataPath + "/progress.data"; // Anti OOP?
    private static string settingsPath = Application.persistentDataPath + "/settings.data"; // Anti OOP?
    private static BinaryFormatter formatter = new BinaryFormatter(); // Anti OOP?
    public enum DataType {
        Progress,
        Settings
    }
    public static void Save(DataType type) {
        FileStream stream = new FileStream(type == DataType.Progress ? progressPath : settingsPath, FileMode.Create);

        object data = type == DataType.Progress ? GameManager.ProgressData : GameManager.SettingsData;
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static object Load(DataType type) {
        if (File.Exists(type == DataType.Progress ? progressPath : settingsPath)) {
            FileStream stream = new FileStream(type == DataType.Progress ? progressPath : settingsPath, FileMode.Open);

            object data = type == DataType.Progress ? formatter.Deserialize(stream) as ProgressData : formatter.Deserialize(stream) as SettingsData; //TODO

            stream.Close();
            return data;
        }
        else {
            Debug.Log("Save file could'nt be found in" + (type == DataType.Progress ? progressPath : settingsPath));
            return null;
        }
    }

    public static void Erase(DataType type) {
        if (File.Exists(type == DataType.Progress ? progressPath : settingsPath)) File.Delete(type == DataType.Progress ? progressPath : settingsPath);
        else Debug.Log("Couldn't find anything in" + (type == DataType.Progress ? progressPath : settingsPath));
    }
}
