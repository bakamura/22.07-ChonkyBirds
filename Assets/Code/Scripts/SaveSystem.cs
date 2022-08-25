using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    private static string path = Application.persistentDataPath + "/progress.data"; // Anti OOP?
    private static BinaryFormatter formatter = new BinaryFormatter(); // Anti OOP?

    public static void SaveProgress() {
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(); // TODO

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadProgress() {
        if (File.Exists(path)) {
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData; //TODO
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file could'nt be found in" + path);
            return null;
        }
    }

    public static void EraseProgress() {
        if (File.Exists(path)) File.Delete(path);
        else Debug.Log("Couldn't find anything in" + path);
    }
}
