using System.Collections.Generic;

[System.Serializable]
public class GameData : System.Object {
	private Dictionary<string, Score> scores = new Dictionary<string, Score>();

	/// <summary>
	/// Sets the Score for a particular level.
	/// </summary>
	/// <param name="levelName">The Level Name</param>
	/// <param name="score">The Score achieved in a session.</param>
	/// <returns>True if a new highscore was achieved, else False.</returns>
	public bool SetScore(string levelName, Score score) {
		// we have not yet kept track of this level...
		if(this.scores.ContainsKey(levelName) == false) {
			this.scores.Add(levelName, score);
			return true;
		}

		// we know we are keeping track of this level...
		// let's compare the stored score with the new one...

		var lastScore = this.scores[levelName];

		if(score.Value > lastScore.Value) {
			this.scores[levelName] = score;

			return true;
		}

		return false;
	}

	/// <summary>
	/// Gets the score based on the level name.
	/// </summary>
	/// <param name="levelName"></param>
	/// <returns></returns>
	public Score GetScore(string levelName) {
		if(this.scores.ContainsKey(levelName) == false) return null;

		return this.scores[levelName];
	}
}

[System.Serializable]
public class Score : System.Object {
	private int value;
	private System.DateTime date;

	public Score(int value, System.DateTime date) {
		this.value = value;
		this.date = date;
	}

	// C# properties...
	public int Value {
		get {
			return this.value;
		} set {
			this.value = value;
		}
	}

	public System.DateTime Date {
		get {
			return this.date;
		} set {
			this.date = value;
		}
	}
}
