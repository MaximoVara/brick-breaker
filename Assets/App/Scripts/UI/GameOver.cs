using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text score;
    public Text best;
    public Button onRetry, onQuit;

    private void Start()
    {
        var gameData = DataManager.GameData;
        var highscore = gameData.GetScore(SceneManager.GetActiveScene().name);
        if(highscore != null)
        {
            
            this.best.text = "Best: " + highscore.Value;
        }
        this.score.text = "Score: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name);
        // this.onRetry.onClick.AddListener(this.OnRetry);
        // this.onQuit.onClick.AddListener(this.OnQuit);
    }
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
