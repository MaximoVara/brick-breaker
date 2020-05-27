using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	public Text score; // The Last Session Score.
	public Text best; // The overall highest score...

	private void Start() {
		var gameData = DataManager.GameData;

		var score = gameData.GetScore("Level 1");

		if(score != null) {
			this.score.text = "Score: " + PlayerPrefs.GetInt("Level 1"); // the current session...
			this.best.text = "Best: " + score.Value; // persistent session data...
		}
	}
}