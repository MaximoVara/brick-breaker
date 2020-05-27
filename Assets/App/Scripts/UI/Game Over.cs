using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text score;
    public Text best;

    private void Start()
    {
        var gameData = DataManager.GameData;
        var score = gameData.GetScore("Level 1");
        if(score != null)
        {
            this.score.text = "Score: " + PlayerPrefs.GetInt("Level 1");
            this.best.text = "Best: " + score.Value;
        }
    }
}
