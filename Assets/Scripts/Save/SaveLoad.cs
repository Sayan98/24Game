using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveLoad {
    
    public static void SaveFile(GetData value) {

        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/PlayerSaveData.txt";
        
        FileStream stream = new FileStream(path,FileMode.OpenOrCreate);
        PlayerData data = new PlayerData(value);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadFile() {
        
        string path = Application.persistentDataPath + "/PlayerSaveData.txt";
        if(File.Exists(path)) {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path,FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else {

            Debug.Log("no file");
            return null;
        }
    }

    public static void DeleteFile() {

        string path = Application.persistentDataPath +"/PlayerSaveData.txt";
        
        if(File.Exists(path))
            File.Delete(path);
    }

    
}
