using UnityEngine;
using Newtonsoft.Json; // this is how we will serialize to JSON.
using System.IO;

public class DataManager : Singleton<DataManager> {
	private GameData gameData = null;

	public static GameData GameData {
		get {
			if(DataManager.Instance == null) return null;

			if(DataManager.Instance.gameData == null) {
				// Load it from disc...
				DataManager.LoadGameData();
			}

			return DataManager.Instance.gameData;
 		}
	}

	static void LoadGameData() {
		var data_folder = DataManager.GetDataFolder();

		var path = data_folder + Path.DirectorySeparatorChar + typeof(GameData).Name + ".json";

		// check if the file for GameData.json exists...
		if(File.Exists(path) == false) {
			// it does not so create a brand new instance...
			DataManager.Instance.gameData = new GameData();
			// now save it to disc....
			DataManager.SaveGameData();

			// we are done...
			return;
		}

		using(var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
			using(var reader = new StreamReader(stream)) {
				var json = reader.ReadToEnd();

				DataManager.Instance.gameData = JsonConvert.DeserializeObject<GameData>(json);
			}
		}
	}

	public static void SaveGameData() {
		var data_folder = DataManager.GetDataFolder();

		var settings = new JsonSerializerSettings();
		settings.Formatting = Formatting.Indented;

		var json = JsonConvert.SerializeObject(DataManager.GameData, settings);

		var path = data_folder + Path.DirectorySeparatorChar + typeof(GameData).Name + ".json";

		// Creates or Opens the file.
		using(var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) {
			// Creates a writable stream
			using(var writer = new StreamWriter(stream)) {
				// Writes a string to the stream.
				writer.WriteLine(json);
			}
		}
	}

	static string GetDataFolder() {
#if UNITY_EDITOR
		var location = Application.dataPath;
#else
		var location = Application.persistentDataPath;
#endif
		var data_folder = location + Path.DirectorySeparatorChar + "data";

		// If the folder does not exist...
		if(Directory.Exists(data_folder) == false) {
			// Create it...
			Directory.CreateDirectory(data_folder);
		}

		return data_folder;
	}
}
