using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // Tallennustiedoston luominen haluttaessa
    public static void SaveGame(GameManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        // Paikka minne tallennustiedosto halutaan tallentaa tallennushetkellä, täytyy olla sama lataamisessa ja tallentaessa.
        string path = Path.Combine(Application.persistentDataPath, "game.data");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    // Tallennetun tiedoston lataaminen
    public static PlayerData LoadGame()
    {
        // Haluttu paikka minne tallennustiedosto on tallennettu laitteessa ja sieltä lataaminen. Täytyy olla sama tallentamisessa ja lataamisessa.
        string path = Path.Combine(Application.persistentDataPath, "game.data");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
}
