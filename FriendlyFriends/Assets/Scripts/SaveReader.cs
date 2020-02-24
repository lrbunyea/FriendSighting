using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveReader
{
    public SaveData s;
    string destination = Application.persistentDataPath + "/save.dat";

    public SaveReader()
    {
        if (File.Exists(destination))
        {
            s = LoadFile();
        }
        else
        {
            s = new SaveData(0);
            SaveFile(s);
        }
    }

    public void SaveFile(SaveData save)
    {
        s = save;
        FileStream file;
        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, s.ToString());
        file.Close();

    }

    public SaveData LoadFile()
    {
        SaveData save;
        FileStream file;
        if (!File.Exists(destination))
        {
            Debug.LogError("error loading file");
            return null;
        }
        file = File.OpenRead(destination);
        BinaryFormatter bf = new BinaryFormatter();
        save = new SaveData((string)bf.Deserialize(file));
        file.Close();
        return save;
    }
}
