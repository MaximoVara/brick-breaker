using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UILevelData: MonoBehaviour
{
    [SerializeField]
    private Text levelName;
    [SerializeField]
    private Text highScore;
    [SerializeField]
    private Image icon;
    public void SetLevelData(LevelDatabase.LevelData levelData)
    {
        this.levelName.text = levelData.Name;
        this.icon.sprite = levelData.Icon;

        var score = DataManager.GameData.GetScore(levelData.Name);
        if (score == null)
        {
            this.highScore.text = "Hight Score: --";
        } else
        {
            this.highScore.text = "Highscore: " + score.Value;
        }
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(
            () => {
                SceneManager.LoadScene(levelData.Name);
            });
    }
}