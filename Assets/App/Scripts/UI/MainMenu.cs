using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Level 1");
    }
}
