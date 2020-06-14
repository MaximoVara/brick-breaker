using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event System.Action<int> onScoreChanged;
    private int score = 0;
    private Brick[] bricks = null;
    private int brickCount = 0;
    private void Start()
    {
        Time.timeScale = 1.00F;
        this.bricks = Object.FindObjectsOfType<Brick>();
        for(int i = 0; i<this.bricks.Length; i++)
        {
            var brick = this.bricks[i];
            
            brick.onHit += this.OnBrickHit;
            brick.onDestroy += this.OnBrickDestroyed;

        }
        this.brickCount = this.bricks.Length;
        GameManager.onScoreChanged?.Invoke(this.score = 0);
    }
    private void OnDestroy()
    {
        Time.timeScale = 1.0F;
    }
    private void OnBrickHit(Brick brick)
    {
        this.AddToScore(1);
    } 
    private void OnBrickDestroyed(Brick brick)
    {
        this.AddToScore(2);
        brick.onHit -= this.OnBrickHit;
        brick.onDestroy -= this.OnBrickDestroyed;
        this.brickCount--;
        if(this.brickCount <= 0)
        {
            OnGameOver();
        }
    }
    private void OnGameOver()
    {
        Debug.Log("You Won!");
        #region Score logic...
        var levelData = LevelDatabase.Instance.GetLevelData(SceneManager.GetActiveScene().name);
        if (levelData != null)
        {
            if (string.IsNullOrEmpty(levelData.NextLevel) == false)
            {
                DataManager.GameData.UnlockLevel(levelData.NextLevel);
            }
        }
        #endregion

        #region Unlock Level Logic...

        #endregion
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, this.score);
        DataManager.GameData.SetScore(SceneManager.GetActiveScene().name, new Score(this.score, System.DateTime.Now));
        DataManager.SaveGameData();


        var asset = Resources.Load<GameOver>("GameOver");
        GameObject.Instantiate(asset.gameObject);

        Time.timeScale = 0.001F;

    }
    private void AddToScore(int amount)
    {
        this.score += amount;
        GameManager.onScoreChanged?.Invoke(this.score);
    }
    
}
