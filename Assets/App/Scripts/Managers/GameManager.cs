using UnityEngine;

public class GameManager : MonoBehaviour {
	public static event System.Action<int> onScoreChanged;

	private int score = 0;
	private Brick[] bricks = null;
	private int brickCount = 0;

	private void Start() {
		Time.timeScale = 1.0F; // Normal time...

		this.bricks = Object.FindObjectsOfType<Brick>();

		for(int i = 0; i < this.bricks.Length; ++i) {
			var brick = this.bricks[i];

			brick.onHit += this.OnBrickHit;
			brick.onDestroyed += this.OnBrickDestroyed;
		}

		this.brickCount = this.bricks.Length;

		GameManager.onScoreChanged?.Invoke(this.score = 0);
	}

	private void OnBrickHit(Brick brick) {
		this.AddToScore(1);
	}

	private void OnBrickDestroyed(Brick brick) {
		brick.onHit -= this.OnBrickHit; // unsubscribe from OnHit...
		brick.onDestroyed -= this.OnBrickDestroyed; // unscribe from OnBrickDestroyed..

		this.brickCount--;

		this.AddToScore(2);

		if(this.brickCount <= 0) {
			this.OnGameOver();
		}
	}

	private void OnGameOver() {
		Debug.Log("You Win!");
		PlayerPrefs.SetInt("Level 1", this.score); // the current session....
		DataManager.GameData.SetScore("Level 1", new Score(this.score, System.DateTime.Now)); // we will attempt to store long term the highest score...
		DataManager.SaveGameData();

		var asset = Resources.Load<GameOver>("Game Over");
		GameObject.Instantiate(asset.gameObject);

		Time.timeScale = 0.001F; // slow down/pause time...
	}

	private void AddToScore(int amount) {
		this.score += amount;

		GameManager.onScoreChanged?.Invoke(this.score);
	}
}
