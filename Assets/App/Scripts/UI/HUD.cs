using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public Text score;

    private void OnEnable()
    {
        GameManager.onScoreChanged += OnScoreChanged;

    }
    private void OnDisable()
    {
        GameManager.onScoreChanged -= OnScoreChanged;
    }
    private void OnScoreChanged(int score)
    {
        this.score.text = "Score: " + score;
    }
}
