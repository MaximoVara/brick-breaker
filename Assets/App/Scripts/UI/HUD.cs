using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	public Text score;

	private void OnEnable() {
		// Subscribe to events...
		GameManager.onScoreChanged += OnScoreChanged;
	}

	private void OnDisable() {
		// Unsubscribe to events...
		GameManager.onScoreChanged -= OnScoreChanged;
	}

	private void OnScoreChanged(int score) {
		this.score.text = "Score: " + score;
	}
}
