using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoSingleton<DataManager>
{
    private GameData gameData = null;
    public static GameData GameData
    {
        get
        {
            if(DataManager.Instance == null) return null;
            if(DataManager.Instance.gameData == null)
            {
                //DataManager.Instance.gameData = new GameData();
                DataManager.LoadGameData();
                DataManager.Instance.gameData.Validate();
            }
            return DataManager.Instance.gameData;
        }

    }

    static void LoadGameData()
    {
        var data_folder = DataManager.GetDataFolder();

        var path = data_folder + Path.DirectorySeparatorChar + typeof(GameData).Name + ".json";
        
        // check if the file for Gamedata.json eists...
        if(File.Exists(path) == false)
        {
            // it does not so create a new instance...
            DataManager.Instance.gameData = new GameData();
            // save it to disc so we don't create it next time...
            DataManager.SaveGameData();
            // we are done...
            return;
        }

        // Open file
        using(var stream = new FileStream(path, FileMode.Open, FileAccess.Read)){
            // Creates reading stream
            using (var reader = new StreamReader(stream))
            {
                // Read the entire thing
                var json = reader.ReadToEnd();
                // Takes the json and puts it into the gameData
                DataManager.Instance.gameData = JsonConvert.DeserializeObject<GameData>(json);
            }
        }
    }
    
    public static void SaveGameData()
    {
        var data_folder = DataManager.GetDataFolder();

        var settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;

        var json = JsonConvert.SerializeObject(DataManager.GameData, settings);
        Debug.Log(json);
        var path = data_folder + Path.DirectorySeparatorChar + typeof(GameData).Name + ".json";

        // Creates or opens the file. 
        using(var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            // Creates a writable stream
            using(var writer = new StreamWriter(stream))
            {
                // Writes a string to the stream.
                writer.WriteLine(json);
            }
        }
    }
    static string GetDataFolder()
    {
#if UNITY_EDITOR
        var location = Application.dataPath;
#else
        var lcoation = Application.persistentDataPath;
#endif
        // Linux/UNIX: directories are / seperated.
        // Windows: Directories are \ seperated.

        // location: path/to/data/path
        Debug.Log("Location: " + location);
        var data_folder = location + Path.DirectorySeparatorChar;
        Debug.Log(data_folder);
        Debug.Log("Does it exist? " + Directory.Exists(data_folder));

        // if the folder does not exist. Create it
        if (Directory.Exists(data_folder) == false)
        {
            Directory.CreateDirectory(data_folder);
        }
        return data_folder;
    }
}
