using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public Text score;

    private void Awake()
    {
        GameManager.onScoreChanged += OnScoreChanged;

    }
    private void OnScoreChanged(int score)
    {
        this.score.text = "Score: " + score;
    }
}
