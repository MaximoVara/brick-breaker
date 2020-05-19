using UnityEngine;

public class GameManager : MonoBehaviour {
	public static event System.Action<int> onScoreChanged;

	private int score = 0;
	private Brick[] bricks = null;
	private int brickCount = 0;

	private void Start() {
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
			Debug.Log("You Win!");
		}
	}

	private void AddToScore(int amount) {
		this.score += amount;

		GameManager.onScoreChanged?.Invoke(this.score);
	}
}
