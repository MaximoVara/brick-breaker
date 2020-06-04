using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	public Text score; // The Last Session Score.
	public Text best; // The overall highest score...

	private void Start() {
		var gameData = DataManager.GameData;

		var highscore = gameData.GetScore("Level 1");

		if(highscore != null) {
			this.best.text = "Highscore: " + highscore.Value; // persistent session data...
		}

		this.score.text = "Score: " + PlayerPrefs.GetInt("Level 1"); // the current session...
	}

	public void OnRetry() {
		// Reloading the scene...
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void OnQuit() {
		SceneManager.LoadScene("Main");
	}
}